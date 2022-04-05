using System.ComponentModel.DataAnnotations;

namespace BookStore.Data.Entities
{
    public class Publisher
    {
        public int Id { get; set; }

        [Display(Name = "Editorial")]
        [MaxLength(100)]
        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<Book> Books { get; set; }
    }
}
