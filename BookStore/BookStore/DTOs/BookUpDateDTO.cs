using System.ComponentModel.DataAnnotations;

namespace BookStore.DTOs
{
    public class BookUpDateDTO
    {
        //public int Id { get; set; }

        [Display(Name = "Título del libro")]
        [MaxLength(50)]
        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        public string Name { get; set; }
        public decimal Price { get; set; }

        [Display(Name = "Fecha de publicación")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime PublicationDate { get; set; }
        public List<int> AuthorsId { get; set; }
        public int PublisherId { get; set; }
        public int GenreId { get; set; }
    }
}
