namespace SDZ2.Domain.Entities
{
    public class Grade
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public Subject Subject { get; set; }
        public Student Student { get; set; }
    }
}
