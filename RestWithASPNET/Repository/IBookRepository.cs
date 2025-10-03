using RestWithASPNET.Models;

namespace RestWithASPNET.Repository
{
    public interface IBookRepository: IRepository<Book>
    {
        Task<List<Book>> FindByAuthorAsync(string author);
        Task<List<Book>> FindByTitleAsync(string title);
        Task<List<Book>> FindByLaunchDateAsync(DateTime releaseDate);
    }
}
