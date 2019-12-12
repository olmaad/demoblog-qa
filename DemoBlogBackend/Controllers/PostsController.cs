using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoBlogBackend.Services;
using DemoBlogShared.Models;
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

        public class PostReturnBundle
        {
            public Post Post { get; set; }
            public User User { get; set; }
            public Vote Vote { get; set; }
        }

        public PostsController(DataService service)
        {
            mDataService = service;
        }

        // GET: api/Posts
        [HttpGet]
        public PostsReturnBundle Get(long? userId, string date)
        {
            long userIdDefaulted = (userId == null) ? -1 : userId.Value;

            DateTime dateDefaulted = DateTime.UtcNow;

            if (date != null)
            {
                DateTime temp;

                if (DateTime.TryParseExact(date, "yyyy-MM-dd", null, 0, out temp))
                {
                    dateDefaulted = temp;
                }
            }

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
                        where post.Date.Date >= dateDefaulted.Date && post.Date.Date < dateDefaulted.AddDays(1).Date
                        select new
                        {
                            Rating = post.Rating * user.Rating * (p != null ? p.Rating : 1),
                            Post = post,
                            User = user,
                            Personal = p,
                            Vote = v
                        };

            var posts = query.OrderByDescending(o => o.Rating).Select(o => o.Post).ToList();
            var users = query.Select(o => o.User).Distinct().ToList().ToList();
            var votes = query.Where(o => o.Vote != null).Select(o => o.Vote).ToList();

            return new PostsReturnBundle()
            {
                Posts = posts,
                Users = users,
                Votes = votes
            };
        }

        // GET: api/Posts/5
        [HttpGet("{postId}", Name = "GetPost")]
        public PostReturnBundle GetPost(long postId, long? userId)
        {
            long userIdDefaulted = (userId == null) ? -1 : userId.Value;

            var query = from post in mDataService.DbContext.Posts
                        join user in mDataService.DbContext.Users on post.UserId equals user.Id
                        join vote in mDataService.DbContext.Votes on
                        new { UserId = userIdDefaulted, Type = Vote.EntityType.Post, EntityId = post.Id }
                        equals
                        new { vote.UserId, vote.Type, vote.EntityId }
                        into voteJoin
                        from v in voteJoin.DefaultIfEmpty()
                        where post.Id == postId
                        select new
                        {
                            Post = post,
                            User = user,
                            Vote = v
                        };

            var postValue = query.Select(o => o.Post).Single();
            var userValue = query.Select(o => o.User).Single();
            var voteValue = query.Select(o => o.Vote).SingleOrDefault();

            return new PostReturnBundle()
            {
                Post = postValue,
                User = userValue,
                Vote = voteValue
            };
        }

        // POST: api/Posts
        [HttpPost]
        public long Post([FromBody] Post value)
        {
            if (string.IsNullOrEmpty(value.Title) || string.IsNullOrEmpty(value.Content))
            {
                return -1;
            }

            bool userExists = (mDataService.DbContext.Users.Find(value.UserId) != null);

            if (!userExists)
            {
                return -1;
            }

            value.Id = 0;
            value.Date = DateTime.UtcNow;

            mDataService.DbContext.Add(value);
            mDataService.DbContext.SaveChanges();

            return value.Id;
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
