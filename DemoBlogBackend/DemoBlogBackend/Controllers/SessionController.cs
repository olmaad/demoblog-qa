using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using DemoBlogBackend.Models;
using DemoBlogBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace DemoBlogBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private DataService mDataService;
        private SHA256 sha256;

        public class SessionCreateParameters
        {
            public string Login { get; set; }
            public string Password { get; set; }
        }

        public SessionController(DataService service)
        {
            mDataService = service;

            sha256 = SHA256.Create();
        }

        // GET: api/Session
        [HttpGet]
        public IActionResult Get()
        {
            return BadRequest();
        }

        // GET: api/Session/5
        [HttpGet("{id}", Name = "GetSession")]
        public Session Get(Guid id)
        {
            var session = mDataService.DbContext.Sessions.Find(id);

            return session;
        }

        // POST: api/Session
        [HttpPost]
        public Session Post([FromBody] SessionCreateParameters value)
        {
            byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(value.Password));

            var user = mDataService.DbContext.Users.Where(u => u.PasswordHash.SequenceEqual(hashValue)).FirstOrDefault();

            if (user == null)
            {
                return new Session() { Valid = false };
            }
            
            var userSessions = mDataService.DbContext.Sessions.Where(s => s.UserId == user.Id).ToList();

            mDataService.DbContext.Sessions.RemoveRange(userSessions);

            var session = new Session() { Valid = true, UserId = user.Id };

            mDataService.DbContext.Sessions.Add(session);
            mDataService.DbContext.SaveChanges();

            return session;
        }

        // PUT: api/Session/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id)
        {
            return BadRequest();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            var session = mDataService.DbContext.Sessions.Find(id);

            if (session == null)
            {
                return;
            }

            mDataService.DbContext.Sessions.Remove(session);
            mDataService.DbContext.SaveChanges();
        }
    }
}
