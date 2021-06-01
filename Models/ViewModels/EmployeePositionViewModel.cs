using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Models.ViewModels
{
    public class EmployeePositionViewModel
    {
        public EmployeePosition EmployeePositions { get; set; }
        public Employee Employee { get; set; }

        public IEnumerable<EmployeePosition> EmployeePositionList { get; set; } // ! Списко ролей

        public IEnumerable<SelectListItem> EmployeePositionListDropDown { get; set; }
    }
}
