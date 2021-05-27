using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Models.ViewModels
{
    public class DepartmentDivisionViewModel
    {
        public DepartmentDivision DepartmentDivisions { get; set; }
        public Department Department { get; set; }

        public IEnumerable<DepartmentDivision> DepartmentDivisionList { get; set; }

        public IEnumerable<SelectListItem> DepartmentDivisionListDropDown { get; set; }
    }
}
