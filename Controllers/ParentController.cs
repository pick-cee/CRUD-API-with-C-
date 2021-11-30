using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using havis2._0.Models;
using System.Security.Cryptography;
using System.Text;

namespace havis2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentController : Controller
    {
        private readonly havisContext _context;

        public ParentController(havisContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Parent>>> getParent()
        {
            return await _context.parent.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Parent>> getOne(int id)
        {
            var one = await _context.parent.FindAsync(id);
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
        public async Task<ActionResult<Parent>> createParent(Parent parent, string hashPassword)
        {
            var provider = new SHA512CryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(parent.Password);
            hashPassword = Convert.ToBase64String(provider.ComputeHash(bytes));
            parent.Password = hashPassword;

            _context.parent.Add(parent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetParent", new { id = parent.Id }, parent);
        }

        [HttpPost("{login}")]
        public async Task<ActionResult> loginParent(Login login)
        {
            Parent parent = new Parent();
            var provider = new SHA512CryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(login.Password);
            var hashPassword = Convert.ToBase64String(provider.ComputeHash(bytes));
            login.Password = hashPassword;

            var three = await _context.parent.FirstOrDefaultAsync(e => e.Email == login.Email);
            var four = await _context.parent.FirstOrDefaultAsync(e => e.Password == login.Password);
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
        public async Task<ActionResult<Parent>> updateParent(int id, Parent parent)
        {
            if (id != parent.Id)
            {
                return BadRequest();
            }
            _context.Entry(parent).State = EntityState.Modified;
            var one = _context.parent.FirstOrDefault(e => e.Id == id);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
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
        public async Task<ActionResult<Parent>> deleteParent(int id)
        {
            var one = await _context.parent.FindAsync(id);
            if (one == null)
            {
                return NotFound();
            }
            else
            {
                _context.parent.Remove(one);
                await _context.SaveChangesAsync();
            }
            return one;
        }
    }
}
