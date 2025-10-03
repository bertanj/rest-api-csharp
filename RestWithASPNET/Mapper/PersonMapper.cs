using RestWithASPNET.Data.Dto.V1;
using RestWithASPNET.Models;

namespace RestWithASPNET.Mapper
{
    public static class PersonMapper
    {
        public static PersonDto MapToDto(Person entity)
        {
            return new PersonDto
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Address = entity.Address,
                Gender = entity.Gender,
                PhoneNumber = entity.PhoneNumber,
                BirthDate = entity.BirthDate
            };
        }

        public static Person MapToEntity(PersonDto dto)
        {
            return new Person
            {
                Id = dto.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Address = dto.Address,
                Gender = dto.Gender,
                PhoneNumber = dto.PhoneNumber,
                BirthDate = dto.BirthDate
            };
        }
    }
}
