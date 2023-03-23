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
    public class LoginController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;

        public LoginController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        [HttpPost]
        public async Task<IActionResult> LoginUser(User user)
        {
            try
            {
                var users = _databaseContext.Users.ToList();
                var FoundUser = users.Where(
                    x => x.Username == user.Username && x.Password == user.Password
                );
                if(_databaseContext.Users != null){
                    if(FoundUser == null){
                        return BadRequest(new {message = "User not found"});
                    }
                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Username),
                        new Claim(ClaimTypes.Name, user.Username)
                    };
                    ClaimsIdentity ci = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal cp = new ClaimsPrincipal(ci);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, cp);
                    return Ok(new { message = "Login successfully" });
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {result = ex.Message, message = "fail"});
            }
        }
    }
}
