using Domain.Enums;
using FluentValidation;
using System;

namespace Application.Requests.Reminders.Commands
{
    public class ReminderPostPutCommon
    {
        public string Title { get; set; }
        public string To { get; set; }
        public DateTime DateTime { get; set; }
        public Level Level { get; set; }
        public bool IsSent { get; set; }

        public class CommonValidator<T> : AbstractValidator<T> where T : ReminderPostPutCommon
        {
            public CommonValidator()
            {
                RuleFor(x => x.Title)
                    .NotNull()
                    .WithMessage("Title Can't be Null")
                    .MinimumLength(2)
                    .WithMessage("Title Can't be less than 2 chars");

                RuleFor(x => x.DateTime)
                    .NotEmpty()
                    .WithMessage("Date Can't be empty");

                RuleFor(x => x.Level)
                    .IsInEnum()
                    .WithMessage("must select Level from choices");
            }

        }
    }
}
