using System.Collections.Generic;

namespace Models
{
    public class Division
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CabinetNumber { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
