using Domain.Enums;
using System;

namespace Domain.Entities
{
    public class Reminder
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string To { get; set; }
        public DateTime DateTime { get; set; }
        public Level Level { get; set; }
        public bool IsSent { get; set; } 
    }
}
