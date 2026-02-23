using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Liggo.Application.UseCases.Operations.LedgerTransactions.Commands.CreateLedgerTransaction;
using Liggo.Application.UseCases.Operations.LedgerTransactions.Queries.GetLedgerTransactionById;

namespace Liggo.Api.Controllers.Operations;

[ApiController]
[Route("api/operations/[controller]")]
public class LedgerTransactionsController : ControllerBase
{
    private readonly ISender _sender;

    public LedgerTransactionsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLedgerTransactionCommand command)
    {
        var transactionId = await _sender.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = transactionId }, new { Id = transactionId });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var query = new GetLedgerTransactionByIdQuery(id);
        var transaction = await _sender.Send(query);

        if (transaction == null)
            return NotFound(new { message = $"No se encontró la transacción con ID {id}" });

        return Ok(transaction);
    }
}