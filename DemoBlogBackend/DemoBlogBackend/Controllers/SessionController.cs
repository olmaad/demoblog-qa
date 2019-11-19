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

        public class SessionCreateBundle
        {
            public string Login { get; set; }
            public string Password { get; set; }
        }

        public class SessionReturnBundle
        {
            public Session Session { get; set; }
            public User User { get; set; }
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
        public SessionReturnBundle Get(Guid id)
        {
            var session = mDataService.DbContext.Sessions.Find(id);

            if (session == null)
            {
                return new SessionReturnBundle()
                {
                    Session = new Session()
                    {
                        Valid = false
                    }
                };
            }

            var user = mDataService.DbContext.Users.Find(session.UserId);

            return new SessionReturnBundle()
            {
                Session = session,
                User = user
            };
        }

        // POST: api/Session
        [HttpPost]
        public SessionReturnBundle Post([FromBody] SessionCreateBundle value)
        {
            byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(value.Password));

            var user = mDataService.DbContext.Users.Where(u => u.Login == value.Login && u.PasswordHash.SequenceEqual(hashValue)).FirstOrDefault();

            if (user == null)
            {
                return new SessionReturnBundle()
                {
                    Session = new Session()
                    {
                        Valid = false
                    }
                };
            }
            
            var userSessions = mDataService.DbContext.Sessions.Where(s => s.UserId == user.Id).ToList();

            mDataService.DbContext.Sessions.RemoveRange(userSessions);

            var session = new Session() { Valid = true, UserId = user.Id };

            mDataService.DbContext.Sessions.Add(session);
            mDataService.DbContext.SaveChanges();

            return new SessionReturnBundle()
            {
                Session = session,
                User = user
            };
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
