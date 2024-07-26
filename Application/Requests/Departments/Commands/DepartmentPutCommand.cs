using Application.Interfaces;
using Application.Requests.Departments.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Departments.Commands
{
    public class DepartmentPutCommand : DepartmentPostPutCommon , IRequest<DepartmentVm>
    {
        public Guid Id { get; set; }

        public class Validator : CommonValidator<DepartmentPutCommand> { }

        public class Handler : IRequestHandler<DepartmentPutCommand , DepartmentVm>
        {
            private readonly IApplicationDbContext _context;

            private List<string> _allowedExtenstions = new List<string>() { ".jpg", ".png" };
            private long _maxSize = 3145728;

            public Handler(IApplicationDbContext context) => _context = context;

            public async Task<DepartmentVm> Handle(DepartmentPutCommand request , CancellationToken cancellationToken)
            {
                var department = await _context.Departments
                    .FirstOrDefaultAsync(x => x.Id == request.Id , cancellationToken);

                if (department == null)
                    throw new Exception("Department is Not Exists");

                if (!_allowedExtenstions.Contains(Path.GetExtension(request.Logo.FileName)))
                    throw new Exception("Not Support this extenstion");

                if (request.Logo.Length > _maxSize)
                    throw new Exception("Max allowed is 3 MB");

                using var dataStream = new MemoryStream();

                await request.Logo.CopyToAsync(dataStream , cancellationToken);

                department.Name               = request.Name;
                department.Logo               = dataStream.ToArray();
                department.ParentDepartmentId = request.ParentDepartmentId;

                await _context.SaveChangesAsync();

                return DepartmentVm.MapFrom(department);
            }
        }
    }
}
