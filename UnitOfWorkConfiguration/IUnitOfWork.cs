using havis2._0.Repository.AccManRepo;
using havis2._0.Repository.ClassRepository;
using havis2._0.Repository.NurseRepository;
using havis2._0.Repository.ParentRepository;
using havis2._0.Repository.ReportRepository;
using havis2._0.Repository.SecurityRepository;
using havis2._0.Repository.StudentRepository;
using havis2._0.Repository.TeacherRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace havis2._0.UnitOfWorkConfiguration
{
    public interface IUnitOfWork
    {
        IAccManRepo AccMan { get; }
        INurseRepo Nurse { get; }
        IParentRepo Parent { get; }
        ISecurityRepo Security { get; }
        ITeacherRepo Teacher { get; }
        IClassRepo Class { get; }
        IReportRepo Report { get; }
        IStudentRepo Student { get; }
        Task CompleteAsync();
        void Dispose();
    }
}
