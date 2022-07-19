using ChatAppV._0._0._2.Api.HubConfig;
using ChatAppV._0._0._2.Core.DTOS.Inputs.Message;
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
    public class MessagesController : ControllerBase
    {
        public IUnitOfWork _unitOfWork;
        private readonly IHubContext<ChatHub> _hub;
        public MessagesController(IUnitOfWork uniOfWork , IHubContext<ChatHub> hub)
        {
            _hub = hub;
            _unitOfWork = uniOfWork;
             
    }
        [HttpPost("sendMessage")]
        public async Task<IActionResult> SendMessage(MessageInputDto dto)
        {
            var u = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            dto.SenderUserName = u;
            var message = await _unitOfWork.messages.SendMessage(dto);
            var seen = await _unitOfWork.messages.Seen(dto.ConnectionId, u);
        
           
            await _hub.Clients.Groups(dto.ConnectionId.ToString()).SendAsync("ResiveMessage", message );
            return Ok(message);
        }
        [HttpGet("GetMessage/{id}/{indx}")]
        public async Task<IActionResult> GetMessages(Guid id, int indx)
        {
            var result = await _unitOfWork.messages.GetMessages(id);
            if (result == null)
                return BadRequest("Something went Wrong");

            var x = result.Count() - (indx * 20);
            var test = result.Skip(x).Take(20);
            if (x > 0)
            {
                test = result.Skip(x).Take(20);
                return Ok(test);
            }
     
            test = result.Skip(0).Take(20 + x);
            return Ok(test);
           
        }

        [HttpGet("seen/{id}")]
        public async Task<IActionResult> seen(Guid id)
        {
            var u = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _unitOfWork.messages.Seen(id ,u);
            if (result==null)
                return BadRequest("Something went Wrong");
            await _hub.Clients.Groups(id.ToString()).SendAsync("Seen", result , id);
            return Ok(result);
        }
    }


}
