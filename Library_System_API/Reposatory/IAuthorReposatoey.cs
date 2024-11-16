using Library_System_API.DTOS;

namespace Library_System_API.Reposatory
{
    public interface IAuthorReposatoey
    {
        IEnumerable<object> GetAllAuthors();
        object GetAuthor(int id);
        bool isadd(AddAuthorDTO addauthordto);
        bool isupdate(int id,UpdateAuthorDTO updateauthordto);
        bool isdelete(int id);
    }
}
