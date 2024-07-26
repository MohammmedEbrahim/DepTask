using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Reminders.Commands
{
    public class ReminderPostCommand : ReminderPostPutCommon, IRequest<Guid>
    {
       public class Validator : CommonValidator<ReminderPostCommand> { }
       
       public class Handler : IRequestHandler<ReminderPostCommand , Guid>
        {
            private readonly IApplicationDbContext _context;

            public Handler(IApplicationDbContext context) =>  _context = context;

            public async Task<Guid> Handle(ReminderPostCommand request , CancellationToken cancellationToken)
            {
                var reminder = new Reminder()
                {
                    Title       = request.Title,
                    To          = request.To,
                    DateTime    = request.DateTime,
                    Level       = request.Level, 
                    IsSent      = false

                };

                await _context.Reminders.AddAsync(reminder , cancellationToken);
                await _context.SaveChangesAsync();

                return reminder.Id;
            }
        }
    }
}
