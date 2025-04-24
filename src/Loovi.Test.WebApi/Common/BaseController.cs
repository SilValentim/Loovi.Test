using AutoMapper;
using Loovi.Test.Common.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Loovi.Test.WebApi.Common
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IMediator _mediator;
        protected readonly IMapper _mapper;

        public BaseController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        protected IActionResult Created<T>(T data, string message)
        {
            var formattedResponse = ApiResponse<T>.Ok(data, message);

            return StatusCode(201, formattedResponse);
        }

        protected IActionResult Ok<T>(T data, string message)
        {
            var formattedResponse = ApiResponse<T>.Ok(data, message);

            return StatusCode(200, formattedResponse);
        }
    }
}
