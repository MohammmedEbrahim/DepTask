using Application.Interfaces;
using Application.Requests.Reminders.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Reminders.Commands
{
    public class ReminderDeleteCommand : IRequest<ReminderVm>
    {
       public Guid Id {get; set;}
       
       public class Handler : IRequestHandler<ReminderDeleteCommand , ReminderVm>
        {
            private readonly IApplicationDbContext _context;

            public Handler(IApplicationDbContext context) => _context = context;

            public async Task<ReminderVm> Handle(ReminderDeleteCommand request , CancellationToken cancellationToken)
            {
                var reminder = await _context.Reminders
                    .FirstOrDefaultAsync(x => x.Id == request.Id , cancellationToken);

                if (reminder == null)
                    throw new Exception("Reminder is Not Exists");

                _context.Reminders.Remove(reminder);
                await _context.SaveChangesAsync();

                return ReminderVm.MapFrom(reminder);
            }
        }
    }
}
