using System.Collections.Generic;

namespace SDZ2.Domain.Entities
{
    public class Institute
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
