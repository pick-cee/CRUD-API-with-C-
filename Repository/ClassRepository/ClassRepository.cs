using havis2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace havis2._0.Repository.ClassRepository
{
    public class ClassRepository : Repository<Class>, IClassRepo
    {
        public ClassRepository(havisContext context) : base(context)
        { }
    }
}
