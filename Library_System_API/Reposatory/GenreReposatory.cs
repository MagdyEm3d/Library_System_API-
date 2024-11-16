using Library_System_API.Connection;
using Library_System_API.DTOS;
using Library_System_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Library_System_API.Reposatory
{
    public class GenreReposatory:IGenreReposatory
    {
        private readonly ApplicationDbContext _context;

        public GenreReposatory(ApplicationDbContext context)
        {
            _context = context;
        }

        public object GetGenre(int id)
        {
            try
            {
                return _context.Genres.Include(x => x.Books).Select(x => new
                {
                    x.GenreId,
                    x.Name,
                    Books = x.Books.Select(x => new { x.BookId, x.Title, x.PublishedYear }).ToList(),
                }).FirstOrDefault(x => x.GenreId == id);

            }
            catch (Exception ex)
            {
                throw new Exception($"Error {ex.Message}");
            }
        }

        public IEnumerable<object> GetGenres()
        {
            try
            {
                return _context.Genres.Include(x => x.Books).ThenInclude(x => x.Authors).Select(x => new
                {
                    x.GenreId,
                    x.Name,
                    Books = x.Books.Select(x => new {
                        x.BookId,
                        x.Title,
                        x.PublishedYear,
                        Authors = x.Authors.Select(x => new
                        {
                            x.AuthorId,
                            x.AuthorName,
                            x.Phone,
                            x.Email,
                        }).ToList()
                    }).ToList(),

                }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception($"Error {ex.Message}");
            }
        }

        public bool isadd(AddGenreDTO addgenredto)
        {
            try
            {
                var genre = _context.Genres.FirstOrDefault(x => x.Name == addgenredto.Name);

                if (genre != null)
                    return false;
                var books = _context.Books.Where(x => addgenredto.BooksIds.Contains(x.BookId)).ToList();
                if (books.Count != addgenredto.BooksIds.Count)
                    return false;
                var genree = new Genre
                {
                    Name = addgenredto.Name,
                    Books = books,
                };
                _context.Genres.Add(genree);
                _context.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                throw new Exception($"Error {ex.Message}");
            }
        }

        public bool isdelete(int id)
        {
            try
            {
                var genre = _context.Genres.FirstOrDefault(y => y.GenreId == id);
                if (genre == null)
                    return false;
                _context.Genres.Remove(genre);
                _context.SaveChanges();
               return true;

            }
            catch (Exception ex)
            {
                throw new Exception($"Error {ex.Message}");
            }
        }

        public bool isupdate(int id, UpdateGenreDTO updategenredto)
        {
            try
            {
                var genre = _context.Genres.FirstOrDefault(x => x.GenreId == id);
                if (genre == null)
                    return false;
                var books = _context.Books.Where(x => updategenredto.BooksIds.Contains(x.BookId)).ToList();
                if (books.Count != updategenredto.BooksIds.Count)
                    return false;
                genre.Name = updategenredto.Name;
                genre.Books = books;
                _context.Genres.Update(genre);
                _context.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                throw new Exception($"Error {ex.Message}");
            }
        }
    }
}
