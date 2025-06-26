using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QtecAcc.Application.Commands;
using QtecAcc.Application.Queries;

namespace QtecAcc.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JournalController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JournalController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: api/journal
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateJournalCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // GET: api/journal
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetJournalEntriesQuery());
            return Ok(result);
        }
    }
}
