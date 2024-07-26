using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Department
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public byte[] Logo { get; set; }
    }

    public partial class Department
    {
        public Guid? ParentDepartmentId { get; set; }
        public Department ParentDepartment { get; set; }
        public List<Department> SubDepartments { get; set; } = new List<Department>();
    }
}
