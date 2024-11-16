using Library_System_API.Connection;
using Library_System_API.DTOS;
using Library_System_API.Models;
using Library_System_API.Reposatory;
using Library_System_API.Token;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorReposatoey _repo;
        private readonly IToken _token;

        public AuthorController(IAuthorReposatoey repo, IToken token)
        {
            _repo=repo;
            _token = token;
        }
        [HttpGet("GetAllAuthors")]
        public IActionResult GetAllAuthors()
        {
            try
            {
                var authors = _repo.GetAllAuthors();

                return Ok(authors);
            }catch (Exception ex)
            {
                return BadRequest($"Error For GetAuthors{ex.Message}");
            }

        }

        [HttpGet("GetAuthor")]
        public IActionResult GetAuthor(int id)
        {
            try
            {
                var author = _repo.GetAuthor(id);
                if (author == null)
                    return BadRequest("Author Not Found");

                return Ok(author);

            }
            catch (Exception ex)
            {
                return BadRequest($"Error For GetAuthor{ex.Message}");
            }

        }

        [HttpPost("AddAuthor")]
        public IActionResult AddAuthor([FromForm]AddAuthorDTO addauthordto)
        {
            try
            {

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                bool isadd = _repo.isadd(addauthordto);
                if (!isadd)
                    return Unauthorized();
                var token = _token.GenerateJwtToken(addauthordto.AuthorName, addauthordto.Email);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error For AddAuthor{ex.Message}");
            }

        }


        [HttpPut("UpdateAuthor")]
        public IActionResult UpdateAuthor(int id,[FromForm]UpdateAuthorDTO updateauthordto)
        {
            try
            {
                bool isupdated = _repo.isupdate(id, updateauthordto);
                if (!isupdated)
                    return BadRequest("Author Or Book Not Found");
                return Ok("Author Updated Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error For UpdateAuthor{ex.Message}");
            }


        }

        [HttpDelete("DeleteAuthor")]
        public IActionResult DeleteAuthor(int id)
        {
            try
            {
                bool isdelete= _repo.isdelete(id);
                if (!isdelete)
                    return BadRequest("Author Not Found");
                return Ok("Author Deleted Successfully");

            }
            catch (Exception ex)
            {
                return BadRequest($"Error For DeleteAuthor{ex.Message}");
            }

        }

    }
}
