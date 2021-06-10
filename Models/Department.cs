using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string MainPhone { get; set; }

        [Required]
        public string BIN { get; set; }

        public List<Division> Divisions { get; set; }
    }
}
