using System.Collections.Generic;

namespace SDZ2.Domain.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public Institute Institute { get; set; }
        public ICollection<Group> Groups { get; set; }
    }
}
