using Domain.Entities;
using Domain.Enums;
using System;

namespace Application.Requests.Reminders.Models
{
    public class ReminderVm
    {
       public string Title { get; set; }
        public DateTime DateTime { get; set; }
        public Level Level { get; set; }
        public bool IsSent { get; set; }  
        
        public static ReminderVm MapFrom(Reminder source)
          => new()
          {
             Title    = source.Title, 
             DateTime = source.DateTime, 
             Level    = source.Level, 
             IsSent   = source.IsSent, 
          };
    }
}
