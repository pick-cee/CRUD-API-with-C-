using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using havis2._0.Models;
using System.Data.Common;
using System.Text;
using System.Security.Cryptography;

namespace havis2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NursesController : Controller
    {
        private readonly havisContext _context;

        public NursesController(havisContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Nurse>>> getAll()
        {
            return await _context.nurse.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Nurse>> getOne(int id)
        {
            var nurse = await _context.nurse.FindAsync(id);
            if (nurse == null)
            {
                return NotFound();
            }
            else
            {
                return nurse;
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Nurse>> updateNurse(int id, Nurse nurse)
        {
            if (id != nurse.Id)
            {
                return BadRequest();
            }
            _context.Entry(nurse).State = EntityState.Modified;
            var nurse1 = _context.nurse.FirstOrDefault(e => e.Id == id);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (nurse1 == null)
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
        [HttpPost]
        public async Task<ActionResult<Nurse>> createNurse(Nurse nurse, string hashPassword)
        {
            var provider = new SHA512CryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(nurse.Password);
            hashPassword = Convert.ToBase64String(provider.ComputeHash(bytes));
            nurse.Password = hashPassword;

            _context.nurse.Add(nurse);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNurse", new { id = nurse.Id }, nurse);
        }

        [HttpPost("{login}")]
        public async Task<ActionResult> loginNurse(Login login)
        {
            Nurse nurse = new ();
            var provider = new SHA512CryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(login.Password);
            var hashPassword = Convert.ToBase64String(provider.ComputeHash(bytes));
            login.Password = hashPassword;

            var three = await _context.nurse.FirstOrDefaultAsync(e => e.Email == login.Email);
            var four = await _context.nurse.FirstOrDefaultAsync(e => e.Password == login.Password);
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

        [HttpDelete("{id}")]
        public async Task<ActionResult<Nurse>> deleteNurse(int id)
        {
            var nurse = await _context.nurse.FindAsync(id);
            if (nurse == null)
            {
                return NotFound();
            }
            else
            {
                _context.nurse.Remove(nurse);
                await _context.SaveChangesAsync();
            }
            return nurse;
        }
    }
}
