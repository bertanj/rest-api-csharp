using Microsoft.EntityFrameworkCore;
using RestWithASPNET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASPNET.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly AppDbContext _context;

        public PersonRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<List<Person>> FindByNameAsync(string firstName, string lastName)
        {
            return await _context.People
                .Where(p => p.FirstName.Contains(firstName) || p.LastName.Contains(lastName))
                .ToListAsync();
        }

        public async Task<List<Person>> FindByGenderAsync(string gender)
        {
            return await _context.People
                .Where(p => p.Gender.ToLower() == gender.ToLower())
                .ToListAsync();
        }

        public async Task<List<Person>> FindByBirthDateAsync(DateTime birthDate)
        {
            return await _context.People
                .Where(p => p.BirthDate.Date == birthDate.Date)
                .ToListAsync();
        }


        public async Task<List<Person>> FindAllAsync()
        {
            return await _context.People.ToListAsync();
        }

        public async Task<Person> FindByIdAsync(long id)
        {
            return await _context.People.SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Person> CreateAsync(Person person)
        {
            _context.Add(person);
            await _context.SaveChangesAsync();
            return person;
        }

        public async Task<Person> UpdateAsync(Person person)
        {
            var result = await _context.People.SingleOrDefaultAsync(p => p.Id == person.Id);
            if (result == null) return null;

            _context.Entry(result).CurrentValues.SetValues(person);
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task DeleteAsync(long id)
        {
            var result = await _context.People.SingleOrDefaultAsync(p => p.Id == id);
            if (result != null)
            {
                _context.People.Remove(result);
                await _context.SaveChangesAsync();
            }
        }
    }
}