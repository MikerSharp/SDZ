using System.Collections.Generic;

namespace SDZ2.Domain.Entities
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Institute Institute { get; set; }
        public Course Course { get; set; }
        public ICollection<Student> Students { get; set; }

    }
}
