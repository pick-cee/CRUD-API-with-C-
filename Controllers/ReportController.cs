using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using havis2._0.Models;
using havis2._0.UnitOfWorkConfiguration;

namespace havis2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Report>>> getReport()
        {
            return await _unitOfWork.Report.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Report>> getOne(int id)
        {
            var one = await _unitOfWork.Report.Get(id);
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
            await _unitOfWork.Report.Add(report);
            return CreatedAtAction("GetReport", new { id = report.Id }, report);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Report>> updateReport(int id, Report report)
        {
            if (id != report.Id)
            {
                return BadRequest();
            }
            else
            {
                await _unitOfWork.Report.Update(report);
                return NoContent();
            }
        }

        [HttpDelete]
        public async Task<ActionResult<Report>> deleteReport(int id)
        {
            var one = await _unitOfWork.Report.Delete(id);
            if (one == null)
            {
                return NotFound();
            }
            return one;
        }
    }
}


