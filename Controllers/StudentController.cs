using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using havis2._0.Models;

namespace havis2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly havisContext _context;

        public StudentController(havisContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> getStudent()
        {
            return await _context.student.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> getOne(int id)
        {
            var one = await _context.student.FindAsync(id);
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
        public async Task<ActionResult<Student>> createStudent(Student student)
        {
            _context.student.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.Id }, student);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> updateStudent(int id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }
            _context.Entry(student).State = EntityState.Modified;
            var one = _context.student.FirstOrDefault(e => e.Id == id);
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
        public async Task<ActionResult<Student>> deleteStudent(int id)
        {
            var one = await _context.student.FindAsync(id);
            if (one == null)
            {
                return NotFound();
            }
            else
            {
                _context.student.Remove(one);
                await _context.SaveChangesAsync();
            }
            return one;
        }
    }
}
