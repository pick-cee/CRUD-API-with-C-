using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using havis2._0.Models;
using havis2._0.Repository;
using havis2._0.UnitOfWorkConfiguration;

namespace havis2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClassController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Class>>> getClass()
        {
            return await _unitOfWork.Class.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Class>> getOne(int id)
        {
            var one = await _unitOfWork.Class.Get(id);
            if (one == null)
            {
                return NotFound();
            }
            return one;
        }

        [HttpPost]
        public async Task<ActionResult<Class>> createClass(Class class1)
        {
            await _unitOfWork.Class.Add(class1);

            return CreatedAtAction("GetClass", new { id = class1.Id }, class1);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Class>> updateClass(int id, Class class1)
        {
            if (id != class1.Id)
            {
                return BadRequest();
            }
            else
            {
                await _unitOfWork.Class.Update(class1);
                return NoContent();
            }
        }
        [HttpDelete]
        public async Task<ActionResult<Class>> deleteClass(int id)
        {
            var one = await _unitOfWork.Class.Delete(id);
            if (one == null)
            {
                return NotFound();
            }
            return one;
        }
    }
}
