using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DemoBlogBackend.Models;
using DemoBlogBackend.Services;
using Microsoft.AspNetCore.Http;
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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Session/5
        [HttpGet("{id}", Name = "GetSession")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Session
        [HttpPost]
        public Session Post([FromBody] SessionCreateParameters value)
        {
            byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(value.Password));

            var user = mDataService.DbContext.Users.Where(u => u.PasswordHash.SequenceEqual(hashValue)).FirstOrDefault();

            if (user != null)
            {
                return new Session() { Valid = true, UserId = user.Id };
            }
            else
            {
                return new Session() { Valid = false };
            }
        }

        // PUT: api/Session/5
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
