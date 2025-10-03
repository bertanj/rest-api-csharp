using RestWithASPNET.Models;

namespace RestWithASPNET.Repository
{
    public interface IPersonRepository: IRepository<Person>
    {
        Task<List<Person>> FindByNameAsync(string firstName, string lastName);
        Task<List<Person>> FindByGenderAsync(string gender);
        Task<List<Person>> FindByBirthDateAsync(DateTime birthDate);
    }
}
