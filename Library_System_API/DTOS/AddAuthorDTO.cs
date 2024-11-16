using Library_System_API.Models;
using System.ComponentModel.DataAnnotations;

namespace Library_System_API.DTOS
{
    public class AddAuthorDTO
    {
        [Required(ErrorMessage = "Please Enter Your Name")]
        public string AuthorName { get; set; }
        [Required(ErrorMessage = "Please Enter Your Email")]
        [EmailAddress(ErrorMessage = "Please Enter Correct Email Structure")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter Your Phone")]
        [Phone(ErrorMessage = "Please Enter Correct Phone Number")]
        public string Phone { get; set; }

        public List<int> BooksIds { get; set; }=new List<int>();
    }
}
