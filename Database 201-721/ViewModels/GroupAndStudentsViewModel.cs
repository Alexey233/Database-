using Database_201_721.Models;

namespace Database_201_721.ViewModels
{
    public class GroupAndStudentsViewModel
    {
        public Group Group { get; set; } 

        public List<User> Users { get; set; }
    }
}
