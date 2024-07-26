using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Requests.Departments.Models
{
    public class DepartmentDataVm
    {
        public string Name { get; set; }
        public byte[] Logo { get; set; }
        public Guid? ParentDepartmentId { get; set; }
        public List<DepartmentDataVm> SubDepartments {get; set;}
        
        public static DepartmentDataVm MapFrom(Department source)
            => new()
            {
                Name               = source.Name,
                Logo               = source.Logo,
                ParentDepartmentId = source.ParentDepartmentId,
                SubDepartments     = source.SubDepartments.Select(DepartmentDataVm.MapFrom).ToList()
            };
    }
}
