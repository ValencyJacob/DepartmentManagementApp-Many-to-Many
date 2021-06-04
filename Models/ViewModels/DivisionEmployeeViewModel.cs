using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Models.ViewModels
{
    public class DivisionEmployeeViewModel
    {
        public DivisionEmployee DivisionEmployees { get; set; }        
        public Division Division { get; set; }


        public IEnumerable<EmployeePosition> EmployeePositionList { get; set; }

        //0_0
        public IEnumerable<Department> DepartmentList { get; set; }
        //

        public IEnumerable<DivisionEmployee> DivisionEmployeeList { get; set; }
        public IEnumerable<SelectListItem> DivisionEmployeeListDropDown { get; set; }
    }
}
