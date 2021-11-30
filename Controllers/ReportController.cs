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
    public class ReportController : Controller
    {
        private readonly havisContext _context;

        public ReportController(havisContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Report>>> getReport()
        {
            return await _context.report.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Report>> getOne(int id)
        {
            var one = await _context.report.FindAsync(id);
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
        public async Task<ActionResult<Report>> createReport(Report report)
        {
            _context.report.Add(report);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReport", new { id = report.Id }, report);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Report>> updateReport(int id, Report report)
        {
            if (id != report.Id)
            {
                return BadRequest();
            }
            _context.Entry(report).State = EntityState.Modified;
            var one = _context.report.FirstOrDefault(e => e.Id == id);
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
        public async Task<ActionResult<Report>> deleteReport(int id)
        {
            var one = await _context.report.FindAsync(id);
            if (one == null)
            {
                return NotFound();
            }
            else
            {
                _context.report.Remove(one);
                await _context.SaveChangesAsync();
            }
            return one;
        }
    }
}
