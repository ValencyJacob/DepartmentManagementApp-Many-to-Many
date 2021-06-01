using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class DivisionEmployee
    {
        [ForeignKey("Division")]
        public int Division_Id { get; set; }
        public Division Division { get; set; }

        [ForeignKey("Employee")]
        public int Employee_Id { get; set; }
        public Employee Employee { get; set; }
    }
}
