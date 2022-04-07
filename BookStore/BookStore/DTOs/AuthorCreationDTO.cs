using System.ComponentModel.DataAnnotations;

namespace BookStore.DTOs
{
    public class AuthorCreationDTO
    {
        [Display(Name = "Nombre")]
        [MaxLength(50)]
        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        public string FirstName { get; set; }

        [Display(Name = "Apellidos")]
        [MaxLength(100)]
        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        public string LastName { get; set; }
    }
}
