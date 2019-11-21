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
    public class MarkController : ControllerBase
    {
        private DataService mDataService;

        public MarkController(DataService service)
        {
            mDataService = service;
        }

        // GET: api/Mark
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Mark/5
        [HttpGet("{id}", Name = "GetMark")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Mark
        [HttpPost]
        public IActionResult Post([FromBody] Mark value)
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
                case Mark.EntityType.Post:
                    {
                        var post = mDataService.DbContext.Posts.Find(value.EntityId);

                        if (post == null)
                        {
                            return BadRequest();
                        }

                        success = AddMark(post, user, value);

                        break;
                    }
                case Mark.EntityType.Comment:
                    {
                        var comment = mDataService.DbContext.Comments.Find(value.EntityId);

                        if (comment == null)
                        {
                            return BadRequest();
                        }

                        success = AddMark(comment, user, value);

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

        // PUT: api/Mark/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private bool AddMark(IRatingEntity entity, User user, Mark mark)
        {
            if (mDataService.DbContext.Marks.Count(m => m.Type == mark.Type && m.EntityId == mark.EntityId && m.UserId == user.Id) != 0)
            {
                return false;
            }

            mark.Id = 0;
            mark.Value = Math.Clamp(mark.Value, -1, 1);

            mDataService.DbContext.Marks.Add(mark);

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

            if (mark.Value < 0)
            {
                entity.Rating = entity.Rating * RatingWeights.PostMarkToPost;
                user.Rating = user.Rating * RatingWeights.PostMarkToUser;
                personal.Rating = personal.Rating * RatingWeights.PostMarkToPersonal;
            }
            else
            {
                entity.Rating = entity.Rating / RatingWeights.PostMarkToPost;
                user.Rating = user.Rating / RatingWeights.PostMarkToUser;
                personal.Rating = personal.Rating / RatingWeights.PostMarkToPersonal;
            }

            mDataService.DbContext.SaveChanges();

            return true;
        }
    }
}
