using Library_System_API.DTOS;
using Library_System_API.Models;

namespace Library_System_API.Reposatory
{
    public interface IBookReposatory
    {
        IEnumerable<object> GetAllBooks();
        object GetBook(int id);
        bool isadd(AddBookDTO addbookdto);
        bool isupdate(int id,UpdateBookDTO updatebookdto);
        bool isdelete(int id);




    }
}
