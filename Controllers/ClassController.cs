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
    public class ClassController : Controller
    {
        private readonly havisContext _context;

        public ClassController(havisContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Class>>> getClass()
        {
            return await _context.class1.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Class>> getOne(int id)
        {
            var one = await _context.class1.FindAsync(id);
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
        public async Task<ActionResult<Class>> createClass(Class class1)
        {
            _context.class1.Add(class1);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClass", new { id = class1.Id }, class1);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Class>> updateClass(int id, Class class1)
        {
            if (id != class1.Id)
            {
                return BadRequest();
            }
            _context.Entry(class1).State = EntityState.Modified;
            var one = _context.class1.FirstOrDefault(e => e.Id == id);
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
        public async Task<ActionResult<Class>> deleteClass(int id)
        {
            var one = await _context.class1.FindAsync(id);
            if (one == null)
            {
                return NotFound();
            }
            else
            {
                _context.class1.Remove(one);
                await _context.SaveChangesAsync();
            }
            return one;
        }
    }
}
