using RestWithASPNET.Data.Dto.V1;
using RestWithASPNET.Models;

namespace RestWithASPNET.Services.V1
{
    public interface IBookService
    {
        Task<List<BookDTO>> FindByAuthorAsync(string author);
        Task<List<BookDTO>> FindByTitleAsync(string title);
        Task<List<BookDTO>> FindByLaunchDateAsync(DateTime launchDate);
        Task<BookDTO> CreateAsync(BookDTO book);
        Task<BookDTO> FindByIdAsync(long id);
        Task<List<BookDTO>> FindAllAsync();
        Task<BookDTO> UpdateAsync(BookDTO book);
        Task DeleteAsync(long id);

    }
}
