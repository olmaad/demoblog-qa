using System;
using System.Collections.Generic;
using System.Linq;
using DemoBlog.Backend.Services;
using DemoBlog.DataLib.Arguments;
using DemoBlog.DataLib.Bundles;
using DemoBlog.DataLib.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoBlog.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private DataService mDataService;

        public PostsController(DataService service)
        {
            mDataService = service;
        }

        // GET: api/Posts
        [HttpGet]
        public PostListBundle Get(long? userId, string date)
        {
            long userIdDefaulted = (userId == null) ? -1 : userId.Value;

            var dateDefaulted = DateTimeOffset.UtcNow;

            if (date != null)
            {
                DateTimeOffset temp;

                if (DateTimeOffset.TryParseExact(date, "yyyy-MM-dd:zzz", null, System.Globalization.DateTimeStyles.AdjustToUniversal, out temp))
                {
                    dateDefaulted = temp;
                }
            }

            var allposts = mDataService.DbContext.Posts.ToList();

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
                        where post.Date >= dateDefaulted && post.Date < dateDefaulted.AddDays(1)
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

            return new PostListBundle()
            {
                Posts = posts,
                Users = users,
                Votes = votes
            };
        }

        // GET: api/Posts/5
        [HttpGet("{postId}", Name = "GetPost")]
        public PostBundle GetPost(long postId, long? userId)
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

            return new PostBundle()
            {
                Post = postValue,
                User = userValue,
                Vote = voteValue
            };
        }

        // POST: api/Posts
        [HttpPost]
        public long Post([FromBody] PostCreateArguments value)
        {
            if (string.IsNullOrEmpty(value.SessionKey) || string.IsNullOrEmpty(value.Post.Title) || string.IsNullOrEmpty(value.Post.Preview))
            {
                return -1;
            }

            var session = mDataService.DbContext.Sessions.Where(s => s.Key == value.SessionKey).FirstOrDefault();

            if (session == null)
            {
                return -1;
            }

            value.Post.Id = 0;
            value.Post.Date = DateTimeOffset.UtcNow;

            mDataService.DbContext.Add(value.Post);
            mDataService.DbContext.SaveChanges();

            return value.Post.Id;
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
