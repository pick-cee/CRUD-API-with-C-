using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using havis2._0.Models;
using System.Text;
using System.Security.Cryptography;

namespace havis2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : Controller
    {
        private readonly havisContext _context;

        public SecurityController(havisContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Security>>> getSecurity()
        {
            return await _context.security.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Security>> getOneSecurity(int id)
        {
            var sec = await _context.security.FindAsync(id);
            if(sec == null)
            {
                return NotFound();
            }
            else
            {
                return sec;
            }
        }

        [HttpPost]
        public async Task<ActionResult<Security>> createSecurity(Security security, string hashPassword)
        {
            var provider = new SHA512CryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(security.Password);
            hashPassword = Convert.ToBase64String(provider.ComputeHash(bytes));
            security.Password = hashPassword;

            _context.security.Add(security);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSecurity", new { id = security.Id }, security);
        }

        [HttpPost("{login}")]
        public async Task<ActionResult> loginSecurity(Login login)
        {
            Security security = new();
            var provider = new SHA512CryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(login.Password);
            var hashPassword = Convert.ToBase64String(provider.ComputeHash(bytes));
            login.Password = hashPassword;

            var three = await _context.security.FirstOrDefaultAsync(e => e.Email == login.Email);
            var four = await _context.security.FirstOrDefaultAsync(e => e.Password == login.Password);
            if (three != null && four != null)
            {
                return Ok();
            }
            else
            {
                await _context.SaveChangesAsync();
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Security>> updateSecurity(int id, Security security)
        {
            if (id != security.Id)
            {
                return BadRequest();
            }
            _context.Entry(security).State = EntityState.Modified;
            var sec = _context.security.FirstOrDefault(e => e.Id == id);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if (sec == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Security>> deleteSecurity(int id)
        {
            var sec = await _context.security.FindAsync(id);
            if (sec == null)
            {
                return NotFound();
            }
            else
            {
                _context.security.Remove(sec);
                await _context.SaveChangesAsync();
            }
            return sec;
        }
    }
}
