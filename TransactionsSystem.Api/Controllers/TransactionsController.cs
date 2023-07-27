using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransactionsSystem.Domain.Commands.Transactions;
using TransactionsSystem.Domain.Requests.Transactions;

namespace TransactionsSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TransactionsController : ControllerBase
    {
        private readonly ISender _sender;

        public TransactionsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPut("UpdateTransactions")]
        public async Task<IActionResult> UpdateTransactions(UpdateTransactionsCommand transactions, CancellationToken cancellationToken)
        {
            await _sender.Send(transactions, cancellationToken);
            return NoContent();
        }

        [HttpGet("CreateCsv")]
        public async Task<IActionResult> CreateCsv([FromQuery] CreateCsvTransactionsParameters transactions, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(transactions, cancellationToken);
            return Ok(result);
        }

        [HttpPut("EditStatus")]
        public async Task<IActionResult> EditStatus(UpdateTransactionCommand transaction, CancellationToken cancellationToken)
        {
            await _sender.Send(transaction, cancellationToken);
            return NoContent();
        }

        [HttpGet("GetAllTransactions")]
        public async Task<IActionResult> GetAllTransactions([FromQuery] TransactionsQueryParameters transactions, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(transactions, cancellationToken);
            return Ok(result);
        }
    }
}
