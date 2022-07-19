using ChatAppV._0._0._2.Api.HubConfig;
using ChatAppV._0._0._2.Core.DTOS.Inputs.FriendRequist;
using ChatAppV._0._0._2.Core.Interfaces.UnitOfWorks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace ChatAppV._0._0._2.Api.Controllers.ChatControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FriendRequistsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOFWork;
        private readonly IHubContext<ChatHub> _hub;
        public FriendRequistsController(IUnitOfWork unitOFWork , IHubContext<ChatHub> hub)
        {
            _unitOFWork = unitOFWork;
            _hub = hub;
        }

        [HttpPost("SendFriendRequist/{UserName}")]
        public async Task<IActionResult> SendFriendRequists([FromRoute] string UserName)
        {
            var u = User.FindFirstValue(ClaimTypes.NameIdentifier);
            SendFrindRequestDto dto = new SendFrindRequestDto() { Resivername = UserName ,  Sendername = u};
            var result = await _unitOFWork.FrindRequist.SendFriendRequist(dto);
            if (result != string.Empty)
                return BadRequest(result);
            var NoFrindRequists =  await _unitOFWork.FrindRequist.GetFrindRequists(UserName);
            await _hub.Clients.Groups(UserName).SendAsync("FirndRequists", NoFrindRequists.Count() );
            return Ok(new {Message = "Requset sent"});
        }
        [HttpPost("CancelFriendRequist/{UserName}")]
        public async Task<IActionResult> CancelFriendRequists([FromRoute] string UserName)
        {
            var u = User.FindFirstValue(ClaimTypes.NameIdentifier);
            SendFrindRequestDto dto = new SendFrindRequestDto() { Resivername = UserName, Sendername = u };
            var result = await _unitOFWork.FrindRequist.CancelFriendRequist(dto);
            if (result != string.Empty)
                return BadRequest(result);
            var NoFrindRequists = await _unitOFWork.FrindRequist.GetFrindRequists(UserName);
            await _hub.Clients.Groups(UserName).SendAsync("FirndRequists", NoFrindRequists.Count());
            return Ok(new { Message = "Requset Canceld" });
        }
        [HttpGet("FriendRequists")]
        public async Task<IActionResult> FriendRequists()
        {
            var u = User.FindFirstValue(ClaimTypes.NameIdentifier);
           
            var result = await _unitOFWork.FrindRequist.GetFrindRequists(u);
            if (result == null)
                return BadRequest($"somthing went Wrong");
           
            return Ok(result);
        }
        [HttpPost("ResponseFreindRequist")]
        public async Task<IActionResult> ResponseFriendRequist([FromBody] ReplayFriendRequestDto dto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var u = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _unitOFWork.FrindRequist.ResponseFriendRequist(new ResponseFriendRequistDto()
            {
                Id = dto.Id,
                IsFriend = dto.IsFriend,
                UserNmae = u
            });
            if (result != string.Empty)
                return BadRequest(result);
            if (dto.IsFriend)
                return Ok(new { message = "you now freinds" });
            return Ok(new { message = "Requist Canceled" });
        }
        [HttpGet("GetNoRequists")]
        public async Task<IActionResult> GetNoRequists()
        {
            var u = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _unitOFWork.FrindRequist.GetFrindRequists(u);
            if (result == null)
                return BadRequest($"somthing went Wrong");
            return Ok(result.Count());
        }


    }
}
