using System.ComponentModel.DataAnnotations;

namespace Library_System_API.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        [Required(ErrorMessage ="Please Enter Your Name")]
        public string AuthorName { get; set; }
        [Required(ErrorMessage = "Please Enter Your Email")]
        [EmailAddress(ErrorMessage ="Please Enter Correct Email Structure")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter Your Phone")]
        [Phone(ErrorMessage = "Please Enter Correct Phone Number")]
        public string Phone { get; set; }

        public List<Book> Books { get; set; }   
    }
}
