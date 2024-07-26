using Application.Requests.Departments.Commands;
using Application.Requests.Departments.Models;
using Application.Requests.Departments.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DepartmentTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : Controller
    {
        private readonly IMediator _mediator;

        public DepartmentController(IMediator mediator) => this._mediator = mediator;

        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentDataVm>> Get(Guid id) => await _mediator.Send(new DepartmentsGetbyIdQuery { Id = id });

        [HttpPost]
        public async Task<ActionResult<Guid>> Post([FromForm] DepartmentPostCommand command) => await _mediator.Send(command);

        [HttpPut]
        public async Task<ActionResult<DepartmentVm>> Put([FromForm] DepartmentPutCommand command) => await _mediator.Send(command);
    }
}
