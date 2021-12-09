using havis2._0.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using havis2._0.UnitOfWorkConfiguration;

namespace havis2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public TeacherController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teacher>>> getTeacher()
        {
            return await _unitOfWork.Teacher.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Teacher>> getOne(int id)
        {
            var one = await _unitOfWork.Teacher.Get(id);
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

            await _unitOfWork.Teacher.Add(teacher);

            return CreatedAtAction("GetTeacher", new { id = teacher.Id }, teacher);
        }

        [HttpPost("{login}")]
        public async Task<Teacher> loginTeacher(string email, string password)
        {
            return await _unitOfWork.Teacher.login(email, password);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Teacher>> updateTeacher(int id, Teacher teacher)
        {
            if (id != teacher.Id)
            {
                return BadRequest();
            }
            else
            {
                await _unitOfWork.Teacher.Update(teacher);
                return NoContent();
            }
        }
        [HttpDelete]
        public async Task<ActionResult<Teacher>> deleteTeacher(int id)
        {
            var one = await _unitOfWork.Teacher.Delete(id);
            if (one == null)
            {
                return NotFound();
            }
            return one;
        }
    }
}
