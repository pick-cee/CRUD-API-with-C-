using havis2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace havis2._0.Repository.TeacherRepository
{
    public interface ITeacherRepo : IRepository<Teacher>
    {
        Task<Teacher> login(string email, string password);
    }
}
