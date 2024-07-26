using Domain.Entities;
using System;

namespace Application.Requests.Departments.Models
{
    public class DepartmentVm
    {
        public string Name { get; set; }
        public byte[] Logo { get; set; }
        public Guid? ParentDepartmentId { get; set; }

        public static DepartmentVm MapFrom(Department source)
            => new()
            {
                Name               = source.Name,
                Logo               = source.Logo,
                ParentDepartmentId = source.ParentDepartmentId
            };
    }
}
