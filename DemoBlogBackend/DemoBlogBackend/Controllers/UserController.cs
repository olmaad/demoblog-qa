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
    public class UserController : ControllerBase
    {
        private DataService mDataService;

        public UserController(DataService service)
        {
            mDataService = service;
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
        public void Post([FromBody] string value)
        {
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
