using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using havis2._0.Models;
using System.Security.Cryptography;
using System.Text;

namespace havis2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccManController : Controller
    {
        private readonly havisContext _context;

        public AccManController(havisContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccMan>>> getAccMan()
        {
            return await _context.accMan.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccMan>> getOne(int id)
        {
            var one = await _context.accMan.FindAsync(id);
            if (one == null)
            {
                return NotFound();
            }
            else
            {
                return one;
            }
        }

        [HttpPost]
        public async Task<ActionResult<AccMan>> createAccMan(AccMan accMan, string hashPassword)
        {
            var provider = new SHA512CryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(accMan.Password);
            hashPassword = Convert.ToBase64String(provider.ComputeHash(bytes));
            accMan.Password = hashPassword;

            _context.accMan.Add(accMan);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccMan", new { id = accMan.Id }, accMan);
        }

        [HttpPost("{login}")]
        public async Task<ActionResult> loginAccMan(Login login)
        {
            AccMan accman = new AccMan();
            var provider = new SHA512CryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(login.Password);
            var hashPassword = Convert.ToBase64String(provider.ComputeHash(bytes));
            login.Password = hashPassword;

            var three = await _context.accMan.FirstOrDefaultAsync(e => e.Email == login.Email);
            var four = await _context.accMan.FirstOrDefaultAsync(e => e.Password == login.Password);
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
        public async Task<ActionResult<AccMan>> updateAccMan(int id, AccMan accMan)
        {
            if (id != accMan.Id)
            {
                return BadRequest();
            }
            _context.Entry(accMan).State = EntityState.Modified;
            var one = _context.accMan.FirstOrDefault(e => e.Id == id);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if (one == null)
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
        [HttpDelete]
        public async Task<ActionResult<AccMan>> deleteAccMan(int id)
        {
            var one = await _context.accMan.FindAsync(id);
            if(one == null)
            {
                return NotFound();
            }
            else
            {
                _context.accMan.Remove(one);
                await _context.SaveChangesAsync();
            }
            return one;
        }
    }
}
