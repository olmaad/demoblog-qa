using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using DemoBlog.Backend.Services;
using DemoBlog.DataLib.Arguments;
using DemoBlog.DataLib.Bundles;
using DemoBlog.DataLib.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoBlog.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private DataService mDataService;
        private SHA256 sha256;
        private RNGCryptoServiceProvider randomProvider;

        private SessionBundle InvalidSession
        {
            get
            {
                return new SessionBundle()
                {
                    Session = new Session() { Valid = false }
                };
            }
        }

        public SessionController(DataService service)
        {
            mDataService = service;

            sha256 = SHA256.Create();
            randomProvider = new RNGCryptoServiceProvider();
        }

        // GET: api/Session
        [HttpGet]
        public IActionResult Get()
        {
            return BadRequest();
        }

        // GET: api/Session/5
        [HttpGet("{id}", Name = "GetSession")]
        public IActionResult Get(Guid id)
        {
            return BadRequest();
        }

        // POST: api/Session
        [HttpPost]
        public SessionBundle Post([FromBody] SessionCreateArguments value)
        {
            return value.Restore ? RestoreSession(value) : CreateSession(value);
        }

        // PUT: api/Session/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id)
        {
            return BadRequest();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(string key)
        {
            var session = mDataService.DbContext.Sessions.Where(s => s.Key == key).FirstOrDefault();

            if (session == null)
            {
                return;
            }

            mDataService.DbContext.Sessions.Remove(session);
            mDataService.DbContext.SaveChanges();
        }

        private SessionBundle RestoreSession(SessionCreateArguments arguments)
        {
            var session = mDataService.DbContext.Sessions.Where(s => s.RestoreKey == arguments.RestoreKey).FirstOrDefault();

            if (session == null)
            {
                return InvalidSession;
            }

            session.Key = GenerateNewKeyString();

            mDataService.DbContext.SaveChanges();

            var user = mDataService.DbContext.Users.Find(session.UserId);

            return new SessionBundle()
            {
                Session = session,
                User = user
            };
        }

        private SessionBundle CreateSession(SessionCreateArguments arguments)
        {
            byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(arguments.Password));

            var user = mDataService.DbContext.Users.Where(u => u.Login == arguments.Login && u.PasswordHash.SequenceEqual(hashValue)).FirstOrDefault();

            if (user == null)
            {
                return InvalidSession;
            }

            var userSessions = mDataService.DbContext.Sessions.Where(s => s.UserId == user.Id).ToList();

            mDataService.DbContext.Sessions.RemoveRange(userSessions);

            var key = GenerateNewKeyString();
            var restoreKey = GenerateNewKeyString();

            var session = new Session()
            {
                Valid = true,
                UserId = user.Id,
                Key = key,
                RestoreKey = restoreKey
            };

            mDataService.DbContext.Sessions.Add(session);
            mDataService.DbContext.SaveChanges();

            return new SessionBundle()
            {
                Session = session,
                User = user
            };
        }

        private string GenerateNewKeyString()
        {
            byte[] key = new byte[128];

            randomProvider.GetBytes(key);

            StringBuilder builder = new StringBuilder();
            foreach (var it in key)
            {
                builder.Append(it.ToString("x2"));
            }

            return builder.ToString();
        }
    }
}
