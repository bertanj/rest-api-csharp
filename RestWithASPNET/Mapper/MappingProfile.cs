using AutoMapper;
using RestWithASPNET.Data.Dto.V1;
using RestWithASPNET.Models;

namespace RestWithASPNET.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        { 
            CreateMap<Person, PersonDto>().ReverseMap();
            CreateMap<Book, BookDTO>().ReverseMap();
        }
    }
}
