using havis2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace havis2._0.Repository.AccManRepo
{
    public class AccManRepo : Repository<AccMan>, IAccManRepo
    {
        public AccManRepo(havisContext context) : base(context)
        {
        }

        public async Task<AccMan> login(string email, string password)
        {
            var accMan = new AccMan();
            var provider = new SHA512CryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(accMan.Password);
            var hashPassword = Convert.ToBase64String(provider.ComputeHash(bytes));
            accMan.Password = hashPassword;

            var one = await _context.accMan.Where(e => e.Email == accMan.Email && e.Password == accMan.Password).FirstOrDefaultAsync();
            if (one != null)
            {
                return accMan;
            }
            else
            {
                return null;
            }
        }
    }
}
