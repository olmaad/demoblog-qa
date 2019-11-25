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
        public PostsReturnBundle Get(long ? userId)
        {
            var posts = mDataService.DbContext.Posts.ToList();

            var userIds = posts.Select(p => p.UserId).ToHashSet();

            var users = mDataService.DbContext.Users.Where(u => userIds.Contains(u.Id)).ToList().Select(u =>
            {
                var temp = u.Clone() as User;
                temp.PasswordHash = null;

                return temp;
            });

            var postIds = posts.Select(p => p.Id).ToHashSet();

            List<Vote> votes = new List<Vote>();

            if (userId != null)
            {
                votes = mDataService.DbContext.Votes.Where(v => v.Type == Vote.EntityType.Post && v.UserId == userId.Value && postIds.Contains(v.EntityId)).ToList();
            }
            else
            {
                votes = mDataService.DbContext.Votes.Where(v => v.Type == Vote.EntityType.Post && postIds.Contains(v.EntityId)).ToList();
            }

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
