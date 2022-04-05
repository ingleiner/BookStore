using System.ComponentModel.DataAnnotations;

namespace BookStore.Data.Entities
{
    public class Genre
    {
        public int Id { get; set; }

        [Display(Name = "Género literario")]
        [MaxLength(50)]
        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
