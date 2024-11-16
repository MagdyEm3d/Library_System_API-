using Library_System_API.Models;
using System.ComponentModel.DataAnnotations;

namespace Library_System_API.DTOS
{
    public class UpdateBookDTO
    {
        [Required(ErrorMessage = "Please Enter Title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please Enter Year")]
        public DateTime PublishedYear { get; set; }

        public List<int> AuthorsIds { get; set; }= new List<int>();
        public List<int> GenresIds { get; set; } = new List<int>();
    }
}
