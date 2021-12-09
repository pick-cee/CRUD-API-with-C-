using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using havis2._0.Models;
using havis2._0.Repository;
using havis2._0.UnitOfWorkConfiguration;

namespace havis2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly IRepository<Student> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public StudentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public StudentController(IRepository<Student> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> getStudent()
        {
            return await _unitOfWork.Student.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> getOne(int id)
        {
            var one = await _unitOfWork.Student.Get(id);
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
            await _unitOfWork.Student.Add(student);
            return CreatedAtAction("GetStudent", new { id = student.Id }, student);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> updateStudent(int id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }
            else
            {
                await _unitOfWork.Student.Update(student);
                return NoContent();
            }
        }
        [HttpDelete]
        public async Task<ActionResult<Student>> deleteStudent(int id)
        {
            var one = await _unitOfWork.Student.Delete(id);
            if (one == null)
            {
                return NotFound();
            }
            return one;
        }
    }
}
