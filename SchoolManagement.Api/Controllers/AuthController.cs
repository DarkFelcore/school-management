using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Authentication.Login;
using SchoolManagement.Application.Authentication.Register;
using SchoolManagement.Contracts.Authentication;

namespace SchoolManagement.Api.Controllers
{
    public class AuthController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly ISender _sender;

        public AuthController(IMapper mapper, ISender sender)
        {
            _mapper = mapper;
            _sender = sender;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            var command = _mapper.Map<RegisterCommand>(request);
            var result = await _sender.Send(command);

            return result.Match(
                result => Ok(_mapper.Map<AuthenticationResponse>(result)),
                Problem
            );
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginRequest request)
        {
            var command = _mapper.Map<LoginQuery>(request);
            var result = await _sender.Send(command);

            return result.Match(
                result => Ok(_mapper.Map<AuthenticationResponse>(result)),
                Problem
            );
        }
    }
}