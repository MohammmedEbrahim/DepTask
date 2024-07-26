using Application.Requests.Reminders.Commands;
using Application.Requests.Reminders.Models;
using Application.Requests.Reminders.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DepartmentTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReminderController : Controller
    {
        private readonly IMediator _mediator;

        public ReminderController(IMediator mediator) => this._mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<List<ReminderVm>>> GetAll() => await _mediator.Send(new ReminderGetAllQuery());

        [HttpPost]
        public async Task<ActionResult<Guid>> Post([FromBody] ReminderPostCommand command) => await _mediator.Send(command);

        [HttpPut]
        public async Task<ActionResult<ReminderVm>> Put([FromBody] ReminderPutCommand command) => await _mediator.Send(command);

        [HttpDelete("{id}")]
        public async Task<ActionResult<ReminderVm>> Delete(Guid id) => await _mediator.Send(new ReminderDeleteCommand { Id = id });
    }
}
