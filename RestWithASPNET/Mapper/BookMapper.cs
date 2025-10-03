using RestWithASPNET.Data.Dto.V1;
using RestWithASPNET.Models;

namespace RestWithASPNET.Mapper
{
    public static class BookMapper
    {

        public static BookDTO MapToDto(Book entity)
        {
            return new BookDTO
            {
                Id = entity.Id,
                Author = entity.Author,
                LaunchDate = entity.LaunchDate,
                Price = entity.Price,
                Title = entity.Title
            };
        }

        public static Book MapToEntity(BookDTO dto)
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
