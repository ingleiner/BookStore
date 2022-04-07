using System.ComponentModel.DataAnnotations;

namespace BookStore.Users
{
    public class UserCredential
    {
        [Required]
        [EmailAddress]  
        public string Email { get; set; }
       
        [Required]
        public string Password { get; set; }
    }
}
