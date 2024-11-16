using Library_System_API.Connection;
using Library_System_API.DTOS;
using Library_System_API.Models;
using Library_System_API.Reposatory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookReposatory _repo;

        public BookController(IBookReposatory repo)
        {
            _repo = repo;
        }

        [HttpGet("Books")]
        public IActionResult GetBooks()
        {
            try
            {
                var books = _repo.GetAllBooks();
                return Ok(books);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error For GetBooks{ex.Message}");
            }

        }

        [HttpGet("Book")]
        public IActionResult GetBook(int id)
        {
            try
            {
                var book = _repo.GetBook(id);
                if (book == null)
                    return BadRequest("Book Not Found");
                return Ok(book);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error For GetBook{ex.Message}");
            }


        }

        [HttpPost("AddBook")]
        public IActionResult AddBook([FromForm]AddBookDTO addbookdto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                bool isadd = _repo.isadd(addbookdto);
                if (!isadd)
                    return BadRequest("Book or Author or Genre Not Found");
                return Ok("Book Addedd Successfully");
            }catch(Exception ex)
            {
                return BadRequest($"Error For Add{ex.Message}");
            }


        }

        [HttpPut("UpdateBook")]
        public IActionResult UpdateBook(int id,[FromForm]UpdateBookDTO updatebookdto)
        {

            try
            {
                bool isupdate = _repo.isupdate(id, updatebookdto);
                if (!isupdate)
                    return BadRequest("Book or Author or Genre Not Found");
                return Ok("Book Updated Successfully");

            }
            catch (Exception ex)
            {
                return BadRequest($"Error For Update{ex.Message}");
            }


        }

        [HttpDelete("DeleteBook")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                bool isdelete = _repo.isdelete(id);
                if (!isdelete) return BadRequest("Book Not Found");
                return Ok("Book Deleted Successfully");

            }
            catch (Exception ex)
            {
                return BadRequest($"Error For Delete{ex.Message}");
            }

        }
    }
}
