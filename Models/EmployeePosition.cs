using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class EmployeePosition
    {
        [ForeignKey("Employee")]
        public int Employee_Id { get; set; }
        public Employee Employee { get; set; }

        [ForeignKey("Position")]
        public int Position_Id { get; set; }
        public Position Position { get; set; }
    }
}
