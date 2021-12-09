using havis2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace havis2._0.Repository.ParentRepository
{
    public interface IParentRepo : IRepository<Parent>
    {
        Task<Parent> login(string email, string password);
    }
}
