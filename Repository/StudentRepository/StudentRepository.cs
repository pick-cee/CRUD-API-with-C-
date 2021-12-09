using havis2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace havis2._0.Repository.StudentRepository
{
    public class StudentRepository : Repository<Student>, IStudentRepo
    {
        public StudentRepository(havisContext context) : base(context)
        {}
    }
}
