using ChatAppV._0._0._2.Core.Interfaces.UnitOfWorks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChatAppV._0._0._2.Api.Controllers.ChatControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet("Users")]
        public async Task<IActionResult> GetUsers()
        {
            var u = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _unitOfWork.users.GetUsers(u);
            return Ok(result.OrderBy(r => Guid.NewGuid()).Take(5));

        }
        [HttpGet("user/{search}")]
        public async Task<IActionResult> GetUsersSearch([FromRoute] string search)
        {
            var u = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _unitOfWork.users.GetUsersSearch(u, search);
            if (result == null)
                return BadRequest();
            return Ok(result);
        }
    }
}
