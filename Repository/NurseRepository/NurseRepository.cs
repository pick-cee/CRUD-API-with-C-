using havis2._0.Models;
using System;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace havis2._0.Repository.NurseRepository
{
    public class NurseRepository : Repository<Nurse>, INurseRepo
    {
        public NurseRepository(havisContext context) : base(context)
        {
        }

        public async Task<Nurse> login(string email, string password)
        {
            var nurse = new Nurse();
            var provider = new SHA512CryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(nurse.Password);
            var hashPassword = Convert.ToBase64String(provider.ComputeHash(bytes));
            nurse.Password = hashPassword;

            var one = await _context.nurse.Where(e => e.Email == nurse.Email && e.Password == nurse.Password).FirstOrDefaultAsync();
            if (one != null)
            {
                return nurse;
            }
            else
            {
                return null;
            }
        }
    }
}
