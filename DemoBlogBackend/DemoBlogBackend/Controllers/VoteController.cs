using System;
using System.Collections.Generic;
using System.Linq;
using DemoBlogBackend.Models;
using DemoBlogBackend.Rating;
using DemoBlogBackend.Services;
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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Vote/5
        [HttpGet("{id}", Name = "GetVote")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Vote
        [HttpPost]
        public IActionResult Post([FromBody] Vote value)
        {
            if (value.Value == 0)
            {
                return BadRequest();
            }

            var user = mDataService.DbContext.Users.Find(value.UserId);

            if (user == null)
            {
                return BadRequest();
            }

            bool success = false;

            switch (value.Type)
            {
                case Vote.EntityType.Post:
                    {
                        var post = mDataService.DbContext.Posts.Find(value.EntityId);

                        if (post == null)
                        {
                            return BadRequest();
                        }

                        success = AddVote(post, user, value);

                        break;
                    }
                case Vote.EntityType.Comment:
                    {
                        var comment = mDataService.DbContext.Comments.Find(value.EntityId);

                        if (comment == null)
                        {
                            return BadRequest();
                        }

                        success = AddVote(comment, user, value);

                        break;
                    }
                default:
                    {
                        return BadRequest();
                    }
            }

            if (success)
            {
                return Ok();
            }
            else;
            {
                return BadRequest();
            }
        }

        // PUT: api/Vote/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Vote/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private bool AddVote(IRatingEntity entity, User user, Vote vote)
        {
            if (mDataService.DbContext.Votes.Count(m => m.Type == vote.Type && m.EntityId == vote.EntityId && m.UserId == user.Id) != 0)
            {
                return false;
            }

            vote.Id = 0;
            vote.Value = Math.Clamp(vote.Value, -1, 1);

            mDataService.DbContext.Votes.Add(vote);

            var author = mDataService.DbContext.Users.Find(entity.UserId);

            var personal = mDataService.DbContext.PersonalRatings.Where(p => p.UserId == user.Id && p.AuthorId == author.Id).FirstOrDefault();

            if (personal == null)
            {
                personal = new PersonalRating()
                {
                    UserId = user.Id,
                    AuthorId = author.Id
                };

                mDataService.DbContext.PersonalRatings.Add(personal);
            }

            if (vote.Value < 0)
            {
                entity.Rating = entity.Rating * RatingWeights.PostVoteToPost;
                user.Rating = user.Rating * RatingWeights.PostVoteToUser;
                personal.Rating = personal.Rating * RatingWeights.PostVoteToPersonal;
            }
            else
            {
                entity.Rating = entity.Rating / RatingWeights.PostVoteToPost;
                user.Rating = user.Rating / RatingWeights.PostVoteToUser;
                personal.Rating = personal.Rating / RatingWeights.PostVoteToPersonal;
            }

            mDataService.DbContext.SaveChanges();

            return true;
        }
    }
}
