using AutoMapper;
using Loovi.Test.Application.Auth.AuthenticateUser;
using Loovi.Test.Common.Auth.Models;
using Loovi.Test.Common.Responses;
using Loovi.Test.WebApi.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Loovi.Test.WebApi.Features.Auth
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : BaseController
    {
        public AuthController(IMediator mediator, IMapper mapper)
            :base(mediator, mapper)
        {

        }

        /// <summary>
        /// Realiza autenticação de usuário.
        /// </summary>
        [HttpPost("login")]
        [ProducesResponseType(typeof(ApiResponse<AuthResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<AuthResponse>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] AuthRequest request)
        {
            var result = await _mediator.Send(new AuthenticateUserCommand(request.Username, request.Password));
            return Ok(
                ApiResponse<AuthResponse>.Ok(_mapper.Map<AuthResponse>(result)),
                "Authentication successful");
        }
    }
}
