namespace Database_201_721.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string CourseName { get; set; }

        public List<Group> Groups { get; set; } = new List<Group>();

    }
}
