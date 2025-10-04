using AutoMapper;
using RestWithASPNET.Data.Dto.V1; 
using RestWithASPNET.Models;
using RestWithASPNET.Repository;
using RestWithASPNET.Mapper;
using RestWithASPNET.Exceptions;
namespace RestWithASPNET.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _repository;
        private readonly ILogger<PersonService> _logger;
        private readonly IMapper _mapper;

        public PersonService(IPersonRepository repository, ILogger<PersonService> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<PersonDto>> FindAllAsync()
        {
            _logger.LogInformation("Finding all people");
            var persons = await _repository.FindAllAsync();
            return _mapper.Map<List<PersonDto>>(persons);
        }

        public async Task<PersonDto> FindByIdAsync(long id)
        {
            _logger.LogInformation("Finding one person");
            var entity = await _repository.FindByIdAsync(id);
            if (entity == null) throw new ResourceNotFoundException("Person not found");
            return _mapper.Map<PersonDto>(entity);
        }

        public async Task<PersonDto> CreateAsync(PersonDto personDto)
        {
            if (personDto == null) throw new RequiredObjectIsNullException();
            _logger.LogInformation("Creating one person");

            var entity = _mapper.Map<Person>(personDto);
            var createdEntity = await _repository.CreateAsync(entity);

            return _mapper.Map<PersonDto>(createdEntity);
        }

        public async Task<PersonDto> UpdateAsync(PersonDto personDto)
        {
            if (personDto == null) throw new RequiredObjectIsNullException();
            _logger.LogInformation("Updating one person");

            var entity = await _repository.FindByIdAsync(personDto.Id);
            if (entity == null) throw new ResourceNotFoundException("Person not found");

            entity.FirstName = personDto.FirstName;
            entity.LastName = personDto.LastName;
            entity.Address = personDto.Address;
            entity.Gender = personDto.Gender;
            entity.BirthDate = personDto.BirthDate;
            entity.PhoneNumber = personDto.PhoneNumber;

            var updatedEntity = await _repository.UpdateAsync(entity);

            return _mapper.Map<PersonDto>(updatedEntity);
        }

        public async Task DeleteAsync(long id)
        {
            _logger.LogInformation("Deleting one person");
            var entity = await _repository.FindByIdAsync(id);
            if (entity == null) throw new ResourceNotFoundException("Person not found");

            await _repository.DeleteAsync(id);
        }


        public async Task<List<PersonDto>> FindByNameAsync(string firstName, string lastName)
        {
            var persons = await _repository.FindByNameAsync(firstName, lastName);
            return _mapper.Map<List<PersonDto>>(persons);
        }

        public async Task<List<PersonDto>> FindByGenderAsync(string gender)
        {
            var persons = await _repository.FindByGenderAsync(gender);
            return _mapper.Map<List<PersonDto>>(persons);
        }
    }
}