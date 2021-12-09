using havis2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace havis2._0.Repository.AccManRepo
{
    public interface IAccManRepo : IRepository<AccMan>
    {
        Task<AccMan> login(string email, string password);
    }
}
