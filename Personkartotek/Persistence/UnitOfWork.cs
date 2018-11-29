using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Personkartotek.DAL;
using Personkartotek.DAL.IRepositories;
using Personkartotek.Models.Context;
using Personkartotek.Persistence.Repositories;

namespace Personkartotek.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ModelsContext _context;

        public UnitOfWork(ModelsContext context)
        {
            _context = context;
            _People = new PeopleRepository(_context);
        }
        
        public IPeopleRepository _People { get; private set; }



        private IPeopleRepository _peopleRepository;
        public IPeopleRepository PeopleRepository
        {
            get
            {
                if (_peopleRepository == null)
                {
                    return _peopleRepository = new PeopleRepository(_context);
                }

                return _peopleRepository;
            }
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }


        public void Dispose()
        {
            _context.Dispose();
        }


        public bool Exist(int id)
        {
            return _context.Persons.Any(e => e.PersonId == id);
        }
    }
}
