using System.ComponentModel.DataAnnotations;

namespace Library_System_API.Models
{
    public class Genre
    {
        public int GenreId { get; set; }
        [Required(ErrorMessage ="Please Enter GenreName")]
        public string Name { get; set; }

        public List<Book> Books { get; set; }   
    }
}
