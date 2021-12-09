using havis2._0.Models;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace havis2._0.Repository.TeacherRepository
{
    public class TeacherRepo : Repository<Teacher> , ITeacherRepo
    {
        public TeacherRepo(havisContext context) : base(context)
        {}

        public async Task<Teacher> login(string email, string password)
        {
            var teacher = new Teacher();
            var provider = new SHA512CryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(teacher.Password);
            var hashPassword = Convert.ToBase64String(provider.ComputeHash(bytes));
            teacher.Password = hashPassword;

            var one = await _context.accMan.Where(e => e.Email == teacher.Email && e.Password == teacher.Password).FirstOrDefaultAsync();
            if (one != null)
            {
                return teacher;
            }
            else
            {
                return null;
            }
        }
    }
}
