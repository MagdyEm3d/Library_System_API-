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
    public class GenreController : ControllerBase
    {
        private readonly IGenreReposatory _repo;

        public GenreController(IGenreReposatory repo)
        {
            _repo = repo;            
        }

        [HttpGet("GetGenres")]
        public IActionResult GetGenres()
        {
            try
            {
                var genres = _repo.GetGenres();

                return Ok(genres);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error For GetGenres{ex.Message}");
            }

        }

        [HttpGet("GetGenre")]
        public IActionResult GetGenre(int id)
        {
            try
            {
                var genre = _repo.GetGenre(id);
                if (genre == null)
                    return BadRequest("Genre Not Found");
                return Ok(genre);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error For GetGenre{ex.Message}");
            }

        }

        [HttpPost("AddGenre")]
        public IActionResult AddGenre([FromForm]AddGenreDTO addgenredto)
        {

            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                bool isadd = _repo.isadd(addgenredto);
                if (!isadd)
                    return BadRequest("Genre Or Book Not Found");
                return Ok("Genre Addedd Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error For AddGenre{ex.Message}");
            }

        }

        [HttpPut("UpdateGenre")]
        public IActionResult UpdateGenre(int id, [FromForm] UpdateGenreDTO updategenredto)
        {
            try
            {
                bool isupdate = _repo.isupdate(id, updategenredto);
                if (!isupdate)
                    return BadRequest("Genre Or Book Not Found");
                return Ok("Genre Updated Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error For UpdateGenre{ex.Message}");
            }




        }

        [HttpDelete("DeleteGenre")]
        public IActionResult DeleteGenre(int id)
        {
            try
            {
                bool isdelete= _repo.isdelete(id);
                if (!isdelete)
                    return BadRequest("Genre Not Found");
                return Ok("Genre Deleted Successfully");

            }
            catch (Exception ex)
            {
                return BadRequest($"Error For DeleteGenre{ex.Message}");
            }
        }
    }
}
