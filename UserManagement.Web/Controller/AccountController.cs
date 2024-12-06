﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Usermanagement.Application.Register;

namespace UserManagement.Web.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;



        [HttpGet]
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

        [HttpPost]
        public async Task<IActionResult> RegisterationRequest(RegisterationCommand command)
        {
            var result = await _mediator.Send(command);
        }

}
}