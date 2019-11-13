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

        public PostsController(DataService service)
        {
            mDataService = service;
        }

        // GET: api/Posts
        [HttpGet]
        public IEnumerable<Post> Get()
        {
            return mDataService.DbContext.Posts;
        }

        // GET: api/Posts/5
        [HttpGet("{id}", Name = "GetPost")]
        public Post Get(long id)
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
