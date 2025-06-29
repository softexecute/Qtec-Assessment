using MediatR;
using Microsoft.AspNetCore.Mvc;
using QtecAcc.Application.Commands;
using QtecAcc.Application.Queries;

namespace QtecAcc.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: api/accounts
        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // GET: api/accounts
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var accounts = await _mediator.Send(new GetAccountsQuery());
            return Ok(accounts);
        }


        // Delete: api/accounts/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var response = await _mediator.Send(new DeleteAccountCommand() { Id = id });
            return Ok(response);
        }

        [HttpGet("trial-balance")]
        public async Task<IActionResult> TrialBalance()
        {
            var result = await _mediator.Send(new GetTrialBalanceQuery());
            return Ok(result);
        }
    }
}
