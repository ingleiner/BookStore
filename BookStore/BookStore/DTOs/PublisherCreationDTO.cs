using System.ComponentModel.DataAnnotations;

namespace BookStore.DTOs
{
    public class PublisherCreationDTO
    {
        [Display(Name = "Editorial")]
        [MaxLength(100)]
        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        public string Name { get; set; } = string.Empty;
    }
}
