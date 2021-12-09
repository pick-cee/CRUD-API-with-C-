using havis2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace havis2._0.Repository.ReportRepository
{
    public class ReportRepository : Repository<Report>, IReportRepo
    {
        public ReportRepository(havisContext context) : base(context)
        {}
    }
}
