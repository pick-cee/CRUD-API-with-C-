using havis2._0.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace havis2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : Controller
    {
        private readonly havisContext _context;

        public TeacherController(havisContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teacher>>> getTeacher()
        {
            return await _context.teacher.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Teacher>> getOne(int id)
        {
            var one = await _context.teacher.FindAsync(id);
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
        public async Task<ActionResult<Teacher>> createTeacher(Teacher teacher, string hashPassword)
        {
            var provider = new SHA512CryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(teacher.Password);
            hashPassword = Convert.ToBase64String(provider.ComputeHash(bytes));
            teacher.Password = hashPassword;

            _context.teacher.Add(teacher);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTeacher", new { id = teacher.Id }, teacher);
        }

        [HttpPost("{login}")]
        public async Task<ActionResult> loginTeacher(Login login)
        {
            Teacher teacher = new ();
            var provider = new SHA512CryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(login.Password);
            var hashPassword = Convert.ToBase64String(provider.ComputeHash(bytes));
            login.Password = hashPassword;

            var three = await _context.teacher.FirstOrDefaultAsync(e => e.Email == login.Email);
            var four = await _context.teacher.FirstOrDefaultAsync(e => e.Password == login.Password);
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
        public async Task<ActionResult<Teacher>> updateTeacher(int id, Teacher teacher)
        {
            if (id != teacher.Id)
            {
                return BadRequest();
            }
            _context.Entry(teacher).State = EntityState.Modified;
            var one = _context.teacher.FirstOrDefault(e => e.Id == id);
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
        public async Task<ActionResult<Teacher>> deleteTeacher(int id)
        {
            var one = await _context.teacher.FindAsync(id);
            if (one == null)
            {
                return NotFound();
            }
            else
            {
                _context.teacher.Remove(one);
                await _context.SaveChangesAsync();
            }
            return one;
        }
    }
}
