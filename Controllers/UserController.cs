using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using webapi.DBContext;
using webapi.Models;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;

        public UserController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        [Authorize]
        [HttpGet]
        public IActionResult GetUsers()
        {
            try
            {
                var users = _databaseContext.Users.ToList();
                return Ok(new { result = users, message = "success" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {result = ex.Message, message = "fail"});
            }
            
        }
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetUserbyID(int id)
        {
            try
            {
                var user = _databaseContext.Users.SingleOrDefault(o => o.Id == id);
                return Ok(new { result = user, message = "success" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {result = ex.Message, message = "fail"});
            }
            
        }
        [HttpPost]
        public IActionResult AddUsers(User user)
        {
            try
            {
                _databaseContext.Add(user);
                _databaseContext.SaveChanges();
                return Ok(new { message = "success" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {result = ex.Message, message = "fail"});
            }
        }
        [Authorize]
        [HttpPut]
        public IActionResult UpdateUsers(User u)
        {
            var users = _databaseContext.Update(u);
            _databaseContext.SaveChanges();
            return Ok(new { message = "success" });

        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteUsers(int id)
        {
            var u = _databaseContext.Users.SingleOrDefault(o => o.Id == id);
            if(u != null)
            {
                var users = _databaseContext.Remove(u);
                _databaseContext.SaveChanges();
            }
            return Ok(new { message = "success" });
        }
    }
}
