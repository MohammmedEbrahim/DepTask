using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class BackgroundJobsService
    {
        private readonly IApplicationDbContext _context;
        private readonly EmailService _emailService;

        public BackgroundJobsService(IApplicationDbContext context , EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }
        public async Task SendEmailReminders()
        {
            var reminders = await _context.Reminders
                                 .Where(x => x.DateTime.Minute == DateTime.Now.Minute
                                                     && x.DateTime.Hour == DateTime.Now.Hour
                                                     && !x.IsSent)
                                 .ToListAsync();

            if (reminders.Any())
            {
                foreach (var reminder in reminders)
                {
                    await _emailService.SendEmailAsync(reminder.To , "Hi There" , reminder.Title);
                    reminder.IsSent = true;
                }

                await _context.SaveChangesAsync();
            }
        }
    }
}
