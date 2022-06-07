using Microsoft.AspNetCore.Identity;

namespace Database_201_721.Models
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public int Year { get; set; }
        public int? ChangeRequestYear { get; set; }


        public int? GroupId { get; set; }
        public virtual Group Group { get; set; } 
    }
}
