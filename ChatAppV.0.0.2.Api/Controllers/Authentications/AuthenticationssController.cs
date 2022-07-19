using ChatAppV._0._0._2.Core.DTOS.Inputs;
using ChatAppV._0._0._2.Core.DTOS.Inputs.Authentication;
using ChatAppV._0._0._2.Core.Interfaces.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChatAppV._0._0._2.Api.Controllers.Authentications
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationssController : ControllerBase
    {
        private readonly IAuthenticationRepository _auth;
        public AuthenticationssController(IAuthenticationRepository auth)
        {
            _auth = auth;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _auth.Register(dto);
            if(!result.IsAuthenticated)
                return BadRequest(result.Message);
            return Ok(result);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _auth.Login(dto);
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);
            return Ok(result);
        }
        [Authorize]
        [HttpPost("UpdateProfile")]
        public async Task<IActionResult> UpdateProfile(IFormFile image)
        {
            if(image == null)
            {
                return BadRequest("Select Image");
            }
            var u = User.FindFirstValue(ClaimTypes.NameIdentifier);

            UpdateImageDto dto = new UpdateImageDto() { Name = u,Image=image};
            var result = await _auth.UpdateImage(dto);
            if(result == null)
            {
                return BadRequest();
            }
            return Ok(new {image =result});
        }
    }

}
