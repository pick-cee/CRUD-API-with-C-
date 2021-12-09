using havis2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace havis2._0.Repository.SecurityRepository
{
    public interface ISecurityRepo : IRepository<Security>
    {
        Task<Security> login(string email, string password);
    }
}
