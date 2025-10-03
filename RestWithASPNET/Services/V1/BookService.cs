using RestWithASPNET.Data.Dto.V1;
using RestWithASPNET.Models;
using RestWithASPNET.Repository;

namespace RestWithASPNET.Services.V1
{
    public class BookService : IBookService
    {

        private readonly IBookRepository _repository;
        private readonly ILogger<BookService> _logger;

        public BookService(IBookRepository repository, ILogger<BookService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<List<BookDTO>> FindByAuthorAsync(string author)
        {
            var books = await _repository.FindByAuthorAsync(author);
            return books.Select(b => MapToDto(b)).ToList();
        }

        public async Task<List<BookDTO>> FindByTitleAsync(string title)
        {
            var books = await _repository.FindByTitleAsync(title);
            return books.Select(b => MapToDto(b)).ToList();
        }

        public async Task<List<BookDTO>> FindByLaunchDateAsync(DateTime launchDate)
        {
            var books = await _repository.FindByLaunchDateAsync(launchDate);
            return books.Select(b => MapToDto(b)).ToList();
        }

        public async Task<BookDTO> FindByIdAsync(long id)
        {
            _logger.LogInformation("Finding one book");
            var entity = await _repository.FindByIdAsync(id);
            if (entity == null) throw new Exception("Book not found");
            return MapToDto(entity);
        }

        public async Task<List<BookDTO>> FindAllAsync() 
        {
            _logger.LogInformation("Finding all books");
            var books = await _repository.FindAllAsync();
            return books.Select(b => MapToDto(b)).ToList();
        }

        public async Task<BookDTO> CreateAsync(BookDTO bookDto)
        {
            if (bookDto == null) throw new ArgumentNullException(nameof(bookDto));

            _logger.LogInformation("Creating one book");

            var entity = MapToEntity(bookDto);
            var createdEntity = await _repository.CreateAsync(entity);

            return MapToDto(createdEntity);
        }

        public async Task<BookDTO> UpdateAsync(BookDTO bookDto)
        { 
            if (bookDto == null) throw new ArgumentNullException(nameof(bookDto));
            
           _logger.LogInformation("Updating one book");

            var entity = await _repository.FindByIdAsync(bookDto.Id);
            if (entity == null) throw new Exception("Book not found");
            
            entity.Author = bookDto.Author;
            entity.LaunchDate = bookDto.LaunchDate;
            entity.Price = bookDto.Price;
            entity.Title = bookDto.Title;

            var updatedEntity = await _repository.UpdateAsync(entity);

            return MapToDto(updatedEntity);
        }

        public async Task DeleteAsync(long id)
        {
            _logger.LogInformation("Deleting one book");
            var entity = await _repository.FindByIdAsync(id);
            if (entity == null) throw new Exception("Book not found");

            await _repository.DeleteAsync(id);
        }



        private BookDTO MapToDto(Book entity) {
            return new BookDTO
            {
                Id = entity.Id,
                Author = entity.Author,
                LaunchDate = entity.LaunchDate,
                Price = entity.Price,
                Title = entity.Title
            };
        }

        private Book MapToEntity(BookDTO dto) 
        {
            return new Book
            {
                Id = dto.Id,
                Author = dto.Author,
                LaunchDate = dto.LaunchDate,
                Price = dto.Price,
                Title = dto.Title
            };
        }
    }
}
