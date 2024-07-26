using Application.Interfaces;
using Application.Requests.Reminders.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Reminders.Queries
{
    public class ReminderGetAllQuery : IRequest<List<ReminderVm>>
    {
        public class Handler : IRequestHandler<ReminderGetAllQuery , List<ReminderVm>>
        {
            private readonly IApplicationDbContext _context;

            public Handler(IApplicationDbContext context) => _context = context;

            public async Task<List<ReminderVm>> Handle(ReminderGetAllQuery request , CancellationToken cancellationToken)
               => await _context.Reminders
                        .AsNoTracking()
                        .Select(x => ReminderVm.MapFrom(x))
                       .ToListAsync(cancellationToken);
        }
    }
}
