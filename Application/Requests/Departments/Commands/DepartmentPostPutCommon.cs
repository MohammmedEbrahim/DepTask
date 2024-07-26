using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;

namespace Application.Requests.Departments.Commands
{
    public class DepartmentPostPutCommon
    {
        public string Name { get; set; }
        public IFormFile Logo { get; set; }
        public Guid? ParentDepartmentId { get; set; }

        public class CommonValidator<T> : AbstractValidator<T> where T : DepartmentPostPutCommon
        {
            public CommonValidator()
            {
                RuleFor(x => x.Name)
                    .NotNull()
                    .WithMessage("Name Can't be Null")
                    .MinimumLength(2)
                    .WithMessage("Name Can't be less than 2 chars");

                RuleFor(x => x.Logo)
                    .NotNull()
                    .WithMessage("Logo Can't be Null");
            }
        }
    }
}
