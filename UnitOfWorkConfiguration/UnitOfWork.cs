using havis2._0.Models;
using System;
using havis2._0.Repository.AccManRepo;
using havis2._0.Repository.NurseRepository;
using havis2._0.Repository.ParentRepository;
using havis2._0.Repository.SecurityRepository;
using havis2._0.Repository.TeacherRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using havis2._0.Repository.ClassRepository;
using havis2._0.Repository.StudentRepository;
using havis2._0.Repository.ReportRepository;

namespace havis2._0.UnitOfWorkConfiguration
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly havisContext _context;
        public IAccManRepo AccMan { get; private set; }
        public INurseRepo Nurse { get; private set; }
        public IParentRepo Parent { get; private set; }
        public ISecurityRepo Security { get; private set; }
        public ITeacherRepo Teacher { get; private set; }
        public IClassRepo Class { get; private set; }
        public IReportRepo Report { get; private set; }
        public IStudentRepo Student { get; private set; }

        public UnitOfWork(havisContext context)
        {
            _context = context;
            AccMan = new AccManRepo(context);
            Nurse = new NurseRepository(context);
            Parent = new ParentRepository(context);
            Security = new SecurityRepository(context);
            Teacher = new TeacherRepo(context);
            Class = new ClassRepository(context);
            Report = new ReportRepository(context);
            Student = new StudentRepository(context);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
