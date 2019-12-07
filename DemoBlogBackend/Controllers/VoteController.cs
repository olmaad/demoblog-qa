using System;
using System.Collections.Generic;
using System.Linq;
using DemoBlogBackend.Services;
using DemoBlogShared.Models;
using DemoBlogShared.Models.Rating;
using Microsoft.AspNetCore.Mvc;

namespace DemoBlogBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteController : ControllerBase
    {
        private DataService mDataService;

        public VoteController(DataService service)
        {
            mDataService = service;
        }

        // GET: api/Vote
        [HttpGet]
        public IActionResult Get()
        {
            return BadRequest();
        }

        // GET: api/Vote/5
        [HttpGet("{id}", Name = "GetVote")]
        public IActionResult Get(int id)
        {
            return BadRequest();
        }

        // POST: api/Vote
        [HttpPost]
        public IActionResult Post([FromBody] Vote value)
        {
            if (value.Value == 0
                || mDataService.DbContext.Votes.Count(v => v.Type == value.Type && v.EntityId == value.EntityId && v.UserId == value.UserId) != 0)
            {
                return BadRequest();
            }

            var result = UpdateRating(value.Type, value.EntityId, value.UserId, 0, value.Value);

            if (!result)
            {
                return BadRequest();
            }

            value.Id = 0;
            value.Value = Math.Clamp(value.Value, -1, 1);

            mDataService.DbContext.Votes.Add(value);
            mDataService.DbContext.SaveChanges();

            return Ok();
        }

        // PUT: api/Vote/5
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] Vote value)
        {
            if (value.Value == 0)
            {
                return BadRequest();
            }

            var vote = mDataService.DbContext.Votes.Find(id);

            if (vote == null)
            {
                return BadRequest();
            }

            var result = UpdateRating(vote.Type, vote.EntityId, vote.UserId, vote.Value, value.Value);

            if (!result)
            {
                return BadRequest();
            }

            vote.Value = value.Value;

            mDataService.DbContext.SaveChanges();

            return Ok();
        }

        // DELETE: api/Vote/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var vote = mDataService.DbContext.Votes.Find(id);

            if (vote == null)
            {
                return BadRequest();
            }

            var result = UpdateRating(vote.Type, vote.EntityId, vote.UserId, vote.Value, 0);

            if (!result)
            {
                return BadRequest();
            }

            mDataService.DbContext.Votes.Remove(vote);
            mDataService.DbContext.SaveChanges();

            return Ok();
        }

        private PersonalRating GetOrCreatePersonalRating(long userId, long authorId)
        {
            var personal = mDataService.DbContext.PersonalRatings.Where(p => p.UserId == userId && p.AuthorId == authorId).FirstOrDefault();

            if (personal == null)
            {
                personal = new PersonalRating()
                {
                    UserId = userId,
                    AuthorId = authorId
                };

                mDataService.DbContext.PersonalRatings.Add(personal);
            }

            return personal;
        }

        private bool UpdateRating(Vote.EntityType entityType, long entityId, long userId, int from, int to)
        {
            IRatingEntity entity = null;

            switch (entityType)
            {
                case Vote.EntityType.Post:
                    {
                        entity = mDataService.DbContext.Posts.Find(entityId);
                        break;
                    }
                case Vote.EntityType.Comment:
                    {
                        entity = mDataService.DbContext.Comments.Find(entityId);
                        break;
                    }
            }

            if (entity == null)
            {
                return false;
            }

            var user = mDataService.DbContext.Users.Find(userId);

            if (user == null)
            {
                return false;
            }

            var personal = GetOrCreatePersonalRating(userId, entity.UserId);

            RecalculateRating(entity, user, personal, from, to);

            mDataService.DbContext.SaveChanges();

            return true;
        }

        private void RecalculateRating(IRatingEntity entity, User user, PersonalRating personal, int oldValue, int value)
        {
            if (oldValue < 0)
            {
                entity.Rating = entity.Rating / entity.WeightToSelf;
                user.Rating = user.Rating / entity.WeightToUser;
                personal.Rating = personal.Rating / entity.WeightToPersonal;
            }
            else if (oldValue > 0)
            {
                entity.Rating = entity.Rating * entity.WeightToSelf;
                user.Rating = user.Rating * entity.WeightToUser;
                personal.Rating = personal.Rating * entity.WeightToPersonal;
            }

            if (value < 0)
            {
                entity.Rating = entity.Rating * entity.WeightToSelf;
                user.Rating = user.Rating * entity.WeightToUser;
                personal.Rating = personal.Rating * entity.WeightToPersonal;
            }
            else if (value > 0)
            {
                entity.Rating = entity.Rating / entity.WeightToSelf;
                user.Rating = user.Rating / entity.WeightToUser;
                personal.Rating = personal.Rating / entity.WeightToPersonal;
            }
        }
    }
}
