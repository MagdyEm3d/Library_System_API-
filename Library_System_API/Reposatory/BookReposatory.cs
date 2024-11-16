using Library_System_API.Connection;
using Library_System_API.DTOS;
using Library_System_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Library_System_API.Reposatory
{
    public class BookReposatory:IBookReposatory
    {
        private readonly ApplicationDbContext _context;

        public BookReposatory(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<object> GetAllBooks()
        {
           try
           {
                return _context.Books.Include(x => x.Authors).Include(x => x.Genres).Select(x => new
                {
                    x.BookId,
                    x.Title,
                    x.PublishedYear,
                    Authors = x.Authors.Select(x => new { x.AuthorId, x.AuthorName, x.Email, x.Phone }).ToList(),
                    Genres = x.Genres.Select(x => new { x.GenreId, x.Name }).ToList(),
                }).ToList();
           }catch(Exception ex)
           {
                throw new Exception ($"Error {ex.Message}");
           }
        }
        public object GetBook(int id)
        {
            try
            {
               
                var book = _context.Books
                    .Include(x => x.Authors)
                    .Include(x => x.Genres)
                    .Where(x => x.BookId == id)  
                    .Select(x => new
                    {
                        x.BookId,
                        x.Title,
                        x.PublishedYear,
                        Authors = x.Authors.Select(a => new { a.AuthorId, a.AuthorName, a.Email, a.Phone }).ToList(),
                        Genres = x.Genres.Select(g => new { g.GenreId, g.Name }).ToList(),
                    })
                    .FirstOrDefault();

                return book;

                
            }
            catch (Exception ex)
            {
                
                throw new Exception($"Error: {ex.Message}");
            }
        }




        public bool isadd(AddBookDTO addbookdto)
        {
            try
            {
                var checkbook = _context.Books.FirstOrDefault(x => x.Title == addbookdto.Title);
                if (checkbook != null)
                    return false;
                var authors = _context.Authors.Where(x => addbookdto.AuthorsIds.Contains(x.AuthorId)).ToList();
                var genres = _context.Genres.Where(x => addbookdto.GenresIds.Contains(x.GenreId)).ToList();


                if (authors.Count != addbookdto.AuthorsIds.Count)
                    return false;
                if (genres.Count != addbookdto.GenresIds.Count)
                    return false;

                var bookk = new Book
                {
                    Title = addbookdto.Title,
                    PublishedYear = addbookdto.PublishedYear,
                    Authors = authors,
                    Genres = genres
                };
                _context.Books.Add(bookk);
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
                var book = _context.Books.FirstOrDefault(x => x.BookId == id);
                if (book == null) return false;
                _context.Books.Remove(book);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error {ex.Message}");
            }

        }

        public bool isupdate(int id, UpdateBookDTO updatebookdto)
        {
            try
            {
                var book = _context.Books.Include(x=>x.Genres).Include(x=>x.Authors).FirstOrDefault(x => x.BookId == id);
                if (book == null)
                    return false;

                var authors = _context.Authors.Where(x => updatebookdto.AuthorsIds.Contains(x.AuthorId)).ToList();
                var genres = _context.Genres.Where(x => updatebookdto.GenresIds.Contains(x.GenreId)).ToList();

                if (authors.Count != updatebookdto.AuthorsIds.Count) return false;


                if (genres.Count != updatebookdto.GenresIds.Count) return false;

                book.Title = updatebookdto.Title;
                book.PublishedYear = updatebookdto.PublishedYear;
                book.Authors = authors;
                book.Genres = genres;

                _context.Books.Update(book);
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
