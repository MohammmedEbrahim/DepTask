using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Departments.Commands
{
    public class DepartmentPostCommand : DepartmentPostPutCommon , IRequest<Guid>
    {
        public class Validator : CommonValidator<DepartmentPostCommand> { }

        public class Handler : IRequestHandler<DepartmentPostCommand , Guid>
        {
            private readonly IApplicationDbContext _context;

            // Logo Limitation
            private List<string> _allowedExtenstions = new List<string>() { ".jpg", ".png" }; 
            private long _maxSize = 3145728;

            public Handler(IApplicationDbContext context) =>  _context = context;

            public async Task<Guid> Handle(DepartmentPostCommand request , CancellationToken cancellationToken)
            {
                if (!_allowedExtenstions.Contains(Path.GetExtension(request.Logo.FileName)))
                    throw new Exception("Not Support this extenstion");

                if (request.Logo.Length > _maxSize)
                    throw new Exception("Max allowed is 3 MB");

                using var dataStream = new MemoryStream();

                await request.Logo.CopyToAsync(dataStream , cancellationToken);

                var department = new Department()
                {
                    Name               = request.Name,
                    Logo               = dataStream.ToArray(),
                    ParentDepartmentId = request.ParentDepartmentId
                };

                await _context.Departments.AddAsync(department , cancellationToken);
                await _context.SaveChangesAsync();

                return department.Id;

            }
        }
    }
}
