using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransactionsSystem.Domain;
using TransactionsSystem.Domain.Commands.Authentication;
using TransactionsSystem.Domain.Responses.Authentication;

namespace TransactionsSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ISender _sender;

        public AuthenticationController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("Login")]
        [ProducesResponseType(typeof(LoginUserResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> LoginUser(LoginUserCommand user, CancellationToken cancellationToken)
        {
            var response = await _sender.Send(user, cancellationToken);
            return Ok(response);
        }
    }
}
