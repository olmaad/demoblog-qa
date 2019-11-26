﻿using System;
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
    public class CommentController : ControllerBase
    {
        private DataService mDataService;

        public class CommentReturnBundle
        {
            public IEnumerable<Comment> Comments { get; set; }
            public IEnumerable<User> Users { get; set; }
            public IEnumerable<Vote> Votes { get; set; }
        }

        public CommentController(DataService service)
        {
            mDataService = service;
        }

        // GET: api/Comment
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Comment/5
        [HttpGet("{postId}", Name = "GetComments")]
        public CommentReturnBundle Get(long postId, long? userId)
        {
            var commentsQuery = mDataService.DbContext.Comments.Where(c => c.PostId == postId);

            var comments = commentsQuery.ToList();

            var commentIds = commentsQuery.Select(c => c.Id).ToHashSet();

            var userIds = commentsQuery.Select(c => c.UserId).ToHashSet();

            var dbUsers = mDataService.DbContext.Users.Where(u => userIds.Contains(u.Id)).ToList();

            var users = dbUsers.Select(u => { var temp = u.Clone() as User; temp.PasswordHash = null; return temp; }).ToList();

            List<Vote> votes = new List<Vote>();

            if (userId != null)
            {
                votes = mDataService.DbContext.Votes.Where(v => v.Type == Vote.EntityType.Comment && v.UserId == userId.Value && commentIds.Contains(v.EntityId)).ToList();
            }

            return new CommentReturnBundle()
            {
                Comments = comments,
                Users = users,
                Votes = votes
            };
        }

        // POST: api/Comment
        [HttpPost]
        public long Post([FromBody] Comment value)
        {
            if (value.Text == null)
            {
                return -1;
            }

            bool postExists = (mDataService.DbContext.Posts.Find(value.PostId) != null);
            bool userExists = (mDataService.DbContext.Users.Find(value.UserId) != null);

            if (!postExists || !userExists)
            {
                return -1;
            }

            value.Id = 0;

            mDataService.DbContext.Comments.Add(value);
            mDataService.DbContext.SaveChanges();

            return value.Id;
        }

        // PUT: api/Comment/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
