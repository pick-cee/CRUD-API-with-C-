using havis2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace havis2._0.Repository.ParentRepository
{
    public class ParentRepository : Repository<Parent> ,IParentRepo
    {
        public ParentRepository(havisContext context) : base(context)
        {
        }

        public async Task<Parent> login(string email, string password)
        {
            var parent = new Parent();
            var provider = new SHA512CryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(parent.Password);
            var hashPassword = Convert.ToBase64String(provider.ComputeHash(bytes));
            parent.Password = hashPassword;

            var one = await _context.parent.Where(e => e.Email == parent.Email && e.Password == parent.Password).FirstOrDefaultAsync();
            if (one != null)
            {
                return parent;
            }
            else
            {
                return null;
            }
        }
    }
}
