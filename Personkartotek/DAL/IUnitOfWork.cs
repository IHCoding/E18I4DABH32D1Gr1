using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Personkartotek.DAL.IRepositories;

namespace Personkartotek.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IPeopleRepository _People { get; }

        int Complete();

        bool Exist(int id);
    }
}
