using RestWithASPNET.Data.Dto.V1;
using RestWithASPNET.Models;
using RestWithASPNET.Repository;
using RestWithASPNET.Mapper;
using RestWithASPNET.Exceptions;
using AutoMapper;

namespace RestWithASPNET.Services.V1
{
    public class BookService : IBookService
    {

        private readonly IBookRepository _repository;
        private readonly ILogger<BookService> _logger;
        private readonly IMapper _mapper;

        public BookService(IBookRepository repository, ILogger<BookService> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<BookDTO>> FindByAuthorAsync(string author)
        {
            var books = await _repository.FindByAuthorAsync(author);
            return _mapper.Map<List<BookDTO>>(books);
        }

        public async Task<List<BookDTO>> FindByTitleAsync(string title)
        {
            var books = await _repository.FindByTitleAsync(title);
            return _mapper.Map<List<BookDTO>>(books);
        }

        public async Task<List<BookDTO>> FindByLaunchDateAsync(DateTime launchDate)
        {
            var books = await _repository.FindByLaunchDateAsync(launchDate);
            return _mapper.Map<List<BookDTO>>(books);
        }

        public async Task<BookDTO> FindByIdAsync(long id)
        {
            _logger.LogInformation("Finding one book");
            var entity = await _repository.FindByIdAsync(id);
            if (entity == null) throw new ResourceNotFoundException("Book not found");
            return _mapper.Map<BookDTO>(entity);
        }

        public async Task<List<BookDTO>> FindAllAsync() 
        {
            _logger.LogInformation("Finding all books");
            var books = await _repository.FindAllAsync();
            return _mapper.Map<List<BookDTO>>(books);
        }

        public async Task<BookDTO> CreateAsync(BookDTO bookDto)
        {
            if (bookDto == null) throw new RequiredObjectIsNullException();

            _logger.LogInformation("Creating one book");

            var entity = _mapper.Map<Book>(bookDto);
            var createdEntity = await _repository.CreateAsync(entity);

            return _mapper.Map<BookDTO>(createdEntity);
        }

        public async Task<BookDTO> UpdateAsync(BookDTO bookDto)
        { 
            if (bookDto == null) throw new RequiredObjectIsNullException();
            
           _logger.LogInformation("Updating one book");

            var entity = await _repository.FindByIdAsync(bookDto.Id);
            if (entity == null) throw new ResourceNotFoundException("Book not found");
            
            entity.Author = bookDto.Author;
            entity.LaunchDate = bookDto.LaunchDate;
            entity.Price = bookDto.Price;
            entity.Title = bookDto.Title;

            var updatedEntity = await _repository.UpdateAsync(entity);

            return _mapper.Map<BookDTO>(updatedEntity);
        }

        public async Task DeleteAsync(long id)
        {
            _logger.LogInformation("Deleting one book");
            var entity = await _repository.FindByIdAsync(id);
            if (entity == null) throw new ResourceNotFoundException("Book not found");

            await _repository.DeleteAsync(id);
        }
    }
}
