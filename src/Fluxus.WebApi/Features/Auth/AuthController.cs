using AutoMapper;
using Fluxus.Application.Features.Auth.AuthenticateUser;
using Fluxus.WebApi.Common;
using Fluxus.WebApi.Features.Auth.AuthenticateUserFeature;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fluxus.WebApi.Features.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AuthController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<AuthenticateUserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AuthenticateUser([FromBody] AuthenticateUserRequest request, CancellationToken cancellationToken)
        {
            var validator = new AuthenticateUserRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<AuthenticateUserCommand>(request);
            var result = await _mediator.Send(command, cancellationToken);

            var response = _mapper.Map<AuthenticateUserResponse>(result);

            return Ok(new ApiResponseWithData<AuthenticateUserResponse>
            {
                Success = true,
                Message = "Usuário autenticado com sucesso",
                Data = response
            });
        }
    }
}
