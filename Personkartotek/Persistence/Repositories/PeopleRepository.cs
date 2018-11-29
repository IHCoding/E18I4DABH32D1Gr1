using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Personkartotek.DAL.IRepositories;
using Personkartotek.Models;
using Personkartotek.Models.Context;


namespace Personkartotek.Persistence.Repositories
{
    public class PeopleRepository : Repository<Person>, IPeopleRepository
    {
        private readonly ModelsContext _context = new ModelsContext();

        public PeopleRepository(ModelsContext context) : base(context)
        {
            this._context = context;
        }

        
        public IEnumerable<Person> GetAllPersons()
        {
            return _context.Persons.ToList();
        }
        
        public Person GetAllPersonsById(int personId)
        {
            return _context.Persons.Find(personId);
        }
        
        public bool InsertPerson(Person person)
        {
            try
            {
                _context.Persons.Add(person);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdatePerson(Person person)
        {
            try
            {
                _context.Entry(person).State = EntityState.Modified;
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public bool DeletePerson(Person person)
        {
            try
            {
                _context.Entry(person).State = EntityState.Deleted;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeletePerson(int personId)
        {
            try
            {
                var person = GetAllPersonsById(personId);
                DeletePerson(person);
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
