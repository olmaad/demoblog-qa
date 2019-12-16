﻿using System.Collections.Generic;
using System.Linq;
using DemoBlog.Backend.Services;
using DemoBlog.DataLib.Arguments;
using DemoBlog.DataLib.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoBlog.Backend.Controllers
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
        public IActionResult Get()
        {
            return BadRequest();
        }

        // GET: api/Comment/5
        [HttpGet("{postId}", Name = "GetComments")]
        public CommentReturnBundle Get(long postId, long? userId)
        {
            long userIdDefaulted = (userId == null) ? -1 : userId.Value;

            var query = from comment in mDataService.DbContext.Comments
                        join user in mDataService.DbContext.Users on comment.UserId equals user.Id
                        join vote in mDataService.DbContext.Votes on
                        new { UserId = userIdDefaulted, Type = Vote.EntityType.Comment, EntityId = comment.Id }
                        equals
                        new { vote.UserId, vote.Type, vote.EntityId }
                        into voteJoin
                        from v in voteJoin.DefaultIfEmpty()
                        where comment.PostId == postId
                        select new
                        {
                            Comment = comment,
                            User = user,
                            Vote = v
                        };

            var comments = query.Select(o => o.Comment).ToList();
            var users = query.Select(o => o.User).Distinct().ToList().ToList();
            var votes = query.Where(o => o.Vote != null).Select(o => o.Vote).ToList();

            return new CommentReturnBundle()
            {
                Comments = comments,
                Users = users,
                Votes = votes
            };
        }

        // POST: api/Comment
        [HttpPost]
        public long Post([FromBody] CommentCreateArguments value)
        {
            if (string.IsNullOrEmpty(value.SessionKey) || string.IsNullOrEmpty(value.Comment.Text))
            {
                return -1;
            }

            var session = mDataService.DbContext.Sessions.Where(s => s.Key == value.SessionKey).FirstOrDefault();

            if (session == null)
            {
                return -1;
            }

            bool postExists = (mDataService.DbContext.Posts.Find(value.Comment.PostId) != null);
            bool userExists = (mDataService.DbContext.Users.Find(value.Comment.UserId) != null);

            if (!postExists || !userExists)
            {
                return -1;
            }

            value.Comment.Id = 0;

            mDataService.DbContext.Comments.Add(value.Comment);
            mDataService.DbContext.SaveChanges();

            return value.Comment.Id;
        }

        // PUT: api/Comment/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            // TODO
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            // TODO
        }
    }
}
