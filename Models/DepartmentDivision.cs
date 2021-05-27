using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class DepartmentDivision
    {
        [ForeignKey("Department")]
        public int Department_Id { get; set; }
        public Department Department { get; set; }

        [ForeignKey("Division")]
        public int Division_Id { get; set; }
        public Division Division { get; set; }
    }
}
