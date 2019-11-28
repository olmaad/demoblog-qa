using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoBlogBackend.Models;
using DemoBlogBackend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoBlogBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private DataService mDataService;

        public class PostsReturnBundle
        {
            public IEnumerable<Post> Posts { get; set; }
            public IEnumerable<User> Users { get; set; }
            public IEnumerable<Vote> Votes { get; set; }
        }

        public PostsController(DataService service)
        {
            mDataService = service;
        }

        // GET: api/Posts
        [HttpGet]
        public PostsReturnBundle Get(long? userId)
        {
            long userIdDefaulted = (userId == null) ? -1 : userId.Value;

            var query = from post in mDataService.DbContext.Posts
                        join user in mDataService.DbContext.Users on post.UserId equals user.Id
                        join personal in mDataService.DbContext.PersonalRatings on
                        new { UserId = userIdDefaulted, AuthorId = post.UserId }
                        equals
                        new { personal.UserId, personal.AuthorId }
                        into personaJoin
                        join vote in mDataService.DbContext.Votes on
                        new { UserId = userIdDefaulted, Type = Vote.EntityType.Post, EntityId = post.Id }
                        equals
                        new { vote.UserId, vote.Type, vote.EntityId }
                        into voteJoin
                        from p in personaJoin.DefaultIfEmpty()
                        from v in voteJoin.DefaultIfEmpty()
                        select new
                        {
                            Rating = post.Rating * user.Rating * (p != null ? p.Rating : 1),
                            Post = post,
                            User = user,
                            Personal = p,
                            Vote = v
                        };

            var posts = query.OrderByDescending(o => o.Rating).Select(o => o.Post).ToList();
            var users = query.Select(o => o.User).Distinct().ToList().Select(u =>
            {
                var temp = u.Clone() as User;
                temp.PasswordHash = null;

                return temp;
            }).ToList();
            var votes = query.Where(o => o.Vote != null).Select(o => o.Vote).ToList();

            return new PostsReturnBundle()
            {
                Posts = posts,
                Users = users,
                Votes = votes
            };
        }

        // GET: api/Posts/5
        [HttpGet("{id}", Name = "GetPost")]
        public Post GetPost(long id)
        {
            var post = mDataService.DbContext.Find<Post>(id);

            return post;
        }

        // POST: api/Posts
        [HttpPost]
        public void Post([FromBody] Post value)
        {
            if (string.IsNullOrEmpty(value.Title) || string.IsNullOrEmpty(value.Content))
            {
                return;
            }

            bool userExists = (mDataService.DbContext.Users.Find(value.UserId) != null);

            if (!userExists)
            {
                return;
            }

            value.Id = 0;
            value.Date = DateTime.UtcNow;

            mDataService.DbContext.Add(value);
            mDataService.DbContext.SaveChanges();
        }

        // PUT: api/Posts/5
        [HttpPut("{id}")]
        public void Put(long id, [FromBody] Post value)
        {
            var post = mDataService.DbContext.Find<Post>(id);
            post.Title = value.Title;
            post.Content = value.Content;

            mDataService.DbContext.Update(post);
            mDataService.DbContext.SaveChanges();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var post = mDataService.DbContext.Find<Post>(id);

            mDataService.DbContext.Remove(post);
            mDataService.DbContext.SaveChanges();
        }
    }
}
