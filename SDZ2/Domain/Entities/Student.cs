using System.Collections.Generic;

namespace SDZ2.Domain.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Group Group { get; set; }
        public ICollection<Subject> Subjects { get; set; }
    }
}
