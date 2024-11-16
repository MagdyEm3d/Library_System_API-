using System.ComponentModel.DataAnnotations;

namespace Library_System_API.Models
{
    public class Book
    {
        public int BookId { get; set; }
        [Required(ErrorMessage ="Please Enter Title")]
        public string Title { get; set; }
        [Required(ErrorMessage ="Please Enter Year")]
        public DateTime PublishedYear { get; set; }

        public List<Author> Authors { get; set; }   
        public List<Genre> Genres { get; set; } 
    }
}
