using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using webapi.DBContext;
using webapi.Models;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogoutController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;

        public LogoutController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        [HttpPost]
        public async Task<IActionResult> LogoutUsers()
        {
            try
            {
                await HttpContext.SignOutAsync();
                return StatusCode(200, new { message = "Logout Successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {result = ex.Message, message = "fail"});
            }
        }
    }
}
