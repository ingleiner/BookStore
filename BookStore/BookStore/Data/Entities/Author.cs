using System.ComponentModel.DataAnnotations;

namespace BookStore.Data.Entities
{
    public class Author
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(50)]
        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        public string FirstName { get; set; }

        [Display(Name = "Apellidos")]
        [MaxLength(100)]
        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        public string LastName { get; set; }

        public ICollection<AuthorBook> AuthorsBooks { get; set; }

    }
}
