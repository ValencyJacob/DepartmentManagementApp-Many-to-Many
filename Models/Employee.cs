using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string MiddleName { get; set; }

        public string LastName { get; set; }

        [Required]
        public string IIN { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required]
        public string MobilePhone { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
