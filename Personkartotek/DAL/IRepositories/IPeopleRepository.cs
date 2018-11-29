using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Personkartotek.Models;


namespace Personkartotek.DAL.IRepositories
{
    public interface IPeopleRepository : IRepository<Person>
    {
        IEnumerable<Person> GetAllPersons();

        
        Person GetAllPersonsById(int personId);

        bool InsertPerson(Person person);
        bool UpdatePerson(Person person);
        bool DeletePerson(Person person);
        bool DeletePerson(int personId);

        void Save();
    }
}
