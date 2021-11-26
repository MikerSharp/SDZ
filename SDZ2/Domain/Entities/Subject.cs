using System.Collections.Generic;

namespace SDZ2.Domain.Entities
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Grade> Grades { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
