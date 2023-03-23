using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using webapi.DBContext;
using webapi.Models;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ForgetPasswordController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;

        public ForgetPasswordController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        [HttpPost]
        public IActionResult ForgetPassword(User user)
        {
            var FoundUser = _databaseContext.Users
                .Where(x => x.Username == user.Username && x.Position == user.Position)
                .FirstOrDefault();
            if(_databaseContext.Users != null){
                if(FoundUser == null){
                    return BadRequest(new {message = "User not found"});
                }
                return StatusCode(200, new {Password = FoundUser.Password});
            }else{
                return BadRequest(new {message = "Data not found"});
            }
        }
    }
}
