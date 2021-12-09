using havis2._0.Models;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace havis2._0.Repository.SecurityRepository
{
    public class SecurityRepository : Repository<Security>, ISecurityRepo
    {
        public SecurityRepository(havisContext context) : base(context)
        {
        }

        public async Task<Security> login(string email, string password)
        {
            var security = new Security();
            var provider = new SHA512CryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(security.Password);
            var hashPassword = Convert.ToBase64String(provider.ComputeHash(bytes));
            security.Password = hashPassword;

            var one = await _context.accMan.Where(e => e.Email == security.Email && e.Password == security.Password).FirstOrDefaultAsync();
            if (one != null)
            {
                return security;
            }
            else
            {
                return null;
            }
        }
    }
}
