using System.ComponentModel.DataAnnotations;

namespace BookStore.Data.Entities
{
    public class Book
    {
        public int Id { get; set; }

        [Display(Name = "Título del libro")]
        [MaxLength(50)]
        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        public string Name { get; set; }
        public decimal Price { get; set; }

        [Display(Name = "Fecha de publicación")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime PublicationDate { get; set; }
        public int PublisherId { get; set; }
        public int GenreId { get; set; }
        public ICollection<AuthorBook> AuthorsBooks { get; set; }
        public virtual Genre Genre { get; set; } 
        public virtual Publisher Publisher { get; set; }   

    }
}
