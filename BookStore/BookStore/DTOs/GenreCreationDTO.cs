using System.ComponentModel.DataAnnotations;

namespace BookStore.DTOs
{
    public class GenreCreationDTO
    {
        [Display(Name = "Género literario")]
        [MaxLength(50)]
        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        public string Name { get; set; }
    }
}
