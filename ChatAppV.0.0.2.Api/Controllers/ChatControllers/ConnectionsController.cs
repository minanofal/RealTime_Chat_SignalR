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
    public class ConnectionsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public ConnectionsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet("GetFriends")]
        public async Task<IActionResult> GetFriends()
        {
            var u = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _unitOfWork.Connections.GetAllFriends(u);
            if (result == null)
                return NotFound($"No User With User Name {u} ");
            return Ok(result);
        }
        [HttpGet("Search/{search}")]
        public async Task<IActionResult> SearchFriends(string search)
        {
            var u = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _unitOfWork.Connections.GetFriendSearch(u,search );
            if (result == null)
                return NotFound($"No User With User Name {u} ");
            return Ok(result);
        }
    }
}
