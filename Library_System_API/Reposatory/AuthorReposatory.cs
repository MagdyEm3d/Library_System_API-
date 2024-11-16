using Library_System_API.Connection;
using Library_System_API.DTOS;
using Library_System_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Library_System_API.Reposatory
{
    public class AuthorReposatory : IAuthorReposatoey
    {
        private readonly ApplicationDbContext _context;

        public AuthorReposatory(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<object> GetAllAuthors()
        {
            try
            {
                return _context.Authors.Include(x => x.Books).Select(x => new
                {
                    x.AuthorId,
                    x.AuthorName,
                    x.Email,
                    x.Phone,
                    Books = x.Books.Select(x => new { x.BookId, x.Title, x.PublishedYear }).ToList(),
                }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception($"Error {ex.Message}");
            }

        }

        public object GetAuthor(int id)
        {
            try
            {
                return _context.Authors.Include(x => x.Books).Select(x => new
                {
                    x.AuthorId,
                    x.AuthorName,
                    x.Email,
                    x.Phone,
                    Books = x.Books.Select(x => new { x.BookId, x.Title, x.PublishedYear }).ToList(),

                }).FirstOrDefault(x => x.AuthorId == id);

            }
            catch (Exception ex)
            {
                throw new Exception($"Error {ex.Message}");
            }
        }

        public bool isadd(AddAuthorDTO addauthordto)
        {
            try
            {
                var author = _context.Authors.FirstOrDefault(x => x.AuthorName == addauthordto.AuthorName);
                if (author != null)
                    return false;
                var books = _context.Books.Where(x => addauthordto.BooksIds.Contains(x.BookId)).ToList();
                if (books.Count != addauthordto.BooksIds.Count)
                    return false;

                var authorr = new Author
                {
                    AuthorName = addauthordto.AuthorName,
                    Email = addauthordto.Email,
                    Phone = addauthordto.Phone,
                    Books = books
                };

                _context.Authors.Add(authorr);
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
                var author = _context.Authors.FirstOrDefault(x => x.AuthorId == id);
                if (author == null)
                    return false;
                _context.Authors.Remove(author);
                _context.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                throw new Exception($"Error {ex.Message}");
            }
        }

        public bool isupdate(int id,UpdateAuthorDTO updateauthordto)
        {
            try
            {
                var author = _context.Authors.FirstOrDefault(x => x.AuthorId == id);
                if (author == null)
                    return false;
                var books = _context.Books.Where(x => updateauthordto.BooksIds.Contains(x.BookId)).ToList();
                if (books.Count != updateauthordto.BooksIds.Count) return false;

                author.AuthorName = updateauthordto.AuthorName;
                author.Email = updateauthordto.Email;
                author.Phone = updateauthordto.Phone;
                author.Books = books;
                _context.Authors.Update(author);
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
