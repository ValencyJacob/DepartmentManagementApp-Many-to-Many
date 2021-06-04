using System.Collections.Generic;

namespace Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string MainPhone { get; set; }
        public string BIN { get; set; }

        public List<Division> Divisions { get; set; }
    }
}
