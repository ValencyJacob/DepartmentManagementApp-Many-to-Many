using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Division
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int CabinetNumber { get; set; }

        [Required]
        public int PhoneNumber { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
