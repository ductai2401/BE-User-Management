using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserManagement.MediatR.Commands;

namespace UserManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class SocialLoginController : BaseController
    {
        public IMediator _mediator { get; set; }

        public SocialLoginController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(SocialLoginCommand userLoginCommand)
        {
            userLoginCommand.RemoteIp = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            var result = await _mediator.Send(userLoginCommand);
            if (!result.Success)
            {
                return ReturnFormattedResponse(result);
            }
            return Ok(new
            {
                result.Data.BearerToken
            });
        }
    }
}
