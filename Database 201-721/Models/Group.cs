namespace Database_201_721.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string GroupName { get; set; }


        public List<User> Users { get; set; }


        public List<Course> Courses { get; set; } = new List<Course>();
    }
}
