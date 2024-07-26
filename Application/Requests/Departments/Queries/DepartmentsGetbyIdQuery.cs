using Application.Interfaces;
using Application.Requests.Departments.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Departments.Queries
{
    public class DepartmentsGetbyIdQuery : IRequest<DepartmentDataVm>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<DepartmentsGetbyIdQuery , DepartmentDataVm>
        {
            private readonly IApplicationDbContext _context;

            public Handler(IApplicationDbContext context) => _context = context;

            public async Task<DepartmentDataVm> Handle(DepartmentsGetbyIdQuery request , CancellationToken cancellationToken)
            {
                var department = await _context.Departments
                                       .Include(x => x.SubDepartments)
                                       .FirstOrDefaultAsync(x => x.Id == request.Id , cancellationToken);

                if (department == null)
                    throw new Exception("Department is Not Exists");

                return DepartmentDataVm.MapFrom(department);
            }
        }
    }
}
