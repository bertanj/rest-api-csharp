using RestWithASPNET.Data.Dto.V1; 
using RestWithASPNET.Models;
using RestWithASPNET.Repository;
using RestWithASPNET.Mapper;
namespace RestWithASPNET.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _repository;
        private readonly ILogger<PersonService> _logger;

        public PersonService(IPersonRepository repository, ILogger<PersonService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<List<PersonDto>> FindAllAsync()
        {
            _logger.LogInformation("Finding all people");
            var persons = await _repository.FindAllAsync();
            return persons.Select(p => PersonMapper.MapToDto(p)).ToList();
        }

        public async Task<PersonDto> FindByIdAsync(long id)
        {
            _logger.LogInformation("Finding one person");
            var entity = await _repository.FindByIdAsync(id);
            if (entity == null) throw new Exception("Person not found");
            return PersonMapper.MapToDto(entity);
        }

        public async Task<PersonDto> CreateAsync(PersonDto personDto)
        {
            if (personDto == null) throw new ArgumentNullException(nameof(personDto));
            _logger.LogInformation("Creating one person");

            var entity = PersonMapper.MapToEntity(personDto);
            var createdEntity = await _repository.CreateAsync(entity);

            return PersonMapper.MapToDto(createdEntity);
        }

        public async Task<PersonDto> UpdateAsync(PersonDto personDto)
        {
            if (personDto == null) throw new ArgumentNullException(nameof(personDto));
            _logger.LogInformation("Updating one person");

            var entity = await _repository.FindByIdAsync(personDto.Id);
            if (entity == null) throw new Exception("Person not found");

            entity.FirstName = personDto.FirstName;
            entity.LastName = personDto.LastName;
            entity.Address = personDto.Address;
            entity.Gender = personDto.Gender;
            entity.BirthDate = personDto.BirthDate;
            entity.PhoneNumber = personDto.PhoneNumber;

            var updatedEntity = await _repository.UpdateAsync(entity);

            return PersonMapper.MapToDto(updatedEntity);
        }

        public async Task DeleteAsync(long id)
        {
            _logger.LogInformation("Deleting one person");
            var entity = await _repository.FindByIdAsync(id);
            if (entity == null) throw new Exception("Person not found");

            await _repository.DeleteAsync(id);
        }


        public async Task<List<PersonDto>> FindByNameAsync(string firstName, string lastName)
        {
            var persons = await _repository.FindByNameAsync(firstName, lastName);
            return persons.Select(p => PersonMapper.MapToDto(p)).ToList();
        }

        public async Task<List<PersonDto>> FindByGenderAsync(string gender)
        {
            var persons = await _repository.FindByGenderAsync(gender);
            return persons.Select(p => PersonMapper.MapToDto(p)).ToList();
        }
    }
}