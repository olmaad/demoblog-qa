using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using DemoBlogBackend.Models;
using DemoBlogBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace DemoBlogBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private DataService mDataService;
        private SHA256 sha256;

        public class UserCreateParameters
        {
            public string Login { get; set; }
            public string Password { get; set; }
        }

        public UserController(DataService service)
        {
            mDataService = service;

            sha256 = SHA256.Create();
        }

        // GET: api/User
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "GetUser")]
        public User Get(long id)
        {
            var user = mDataService.DbContext.Find<User>(id).Clone() as User;

            user.PasswordHash = null;

            return user;
        }

        // POST: api/User
        [HttpPost]
        public void Post([FromBody] UserCreateParameters value)
        {
            byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(value.Password));

            var user = new User() { Login = value.Login, PasswordHash = hashValue };

            mDataService.DbContext.Users.Add(user);
            mDataService.DbContext.SaveChanges();
        }

        // PUT: api/User/5
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
