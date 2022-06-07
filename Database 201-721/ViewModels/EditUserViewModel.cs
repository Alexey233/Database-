using Database_201_721.Models;

namespace Database_201_721.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Year { get; set; }


        public int? GroupId { get; set; }
        public List<Group>? Groups { get; set; }
    }
}
