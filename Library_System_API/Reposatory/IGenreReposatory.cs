using Library_System_API.DTOS;

namespace Library_System_API.Reposatory
{
    public interface IGenreReposatory
    {
        IEnumerable<object> GetGenres();
        object GetGenre(int id);
        bool isadd(AddGenreDTO addgenredto);
        bool isupdate(int id,UpdateGenreDTO updategenredto);
        bool isdelete(int id);  
    }
}
