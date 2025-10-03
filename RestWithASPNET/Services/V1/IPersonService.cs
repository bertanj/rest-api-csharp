using RestWithASPNET.Data.Dto.V1;

namespace RestWithASPNET.Services
{
    public interface IPersonService
    {

        Task<List<PersonDto>> FindByNameAsync(string FirstName, string LastName);
        Task<List<PersonDto>> FindByGenderAsync(string gender);
        Task<PersonDto> CreateAsync(PersonDto person);
        Task<PersonDto> FindByIdAsync(long id);
        Task<List<PersonDto>> FindAllAsync();
        Task<PersonDto> UpdateAsync(PersonDto person);
        Task DeleteAsync(long id);
    }
}