using System.Collections.Generic;

namespace Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Division> Divisions { get; set; }
    }
}
