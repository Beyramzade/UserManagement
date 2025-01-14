using MediatR;
using Microsoft.AspNetCore.Mvc;
using Usermanagement.Application.Register;

namespace UserManagement.Web.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }



        [HttpGet("HealthCheck")]
        public object Test()
        {
            var me = new
            {
                mess = "hello",
                ver = "v1.0",
                name = "UserManagement service api"
            };
            return me;
        }

        [HttpPost("RegisterationRequest")]
        public async Task<IActionResult> RegisterationRequest(RegisterationCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }

    }
}
