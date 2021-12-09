using System;
using System.Collections.Generic;
using System.Linq;
using havis2._0.Models;
using System.Threading.Tasks;

namespace havis2._0.Repository.NurseRepository
{
    public interface INurseRepo : IRepository<Nurse>
    {
        Task<Nurse> login(string email, string password);
    }
}
