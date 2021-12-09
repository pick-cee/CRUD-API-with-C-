using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using havis2._0.Models;
using System.Text;
using System.Security.Cryptography;
using havis2._0.UnitOfWorkConfiguration;

namespace havis2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NursesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public NursesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Nurse>>> getAll()
        {
            return await _unitOfWork.Nurse.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Nurse>> getOne(int id)
        {
            var nurse = await _unitOfWork.Nurse.Get(id);
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
            else
            {
                await _unitOfWork.Nurse.Update(nurse);
                return NoContent();
            }
        }
        [HttpPost]
        public async Task<ActionResult<Nurse>> createNurse(Nurse nurse, string hashPassword)
        {
            var provider = new SHA512CryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(nurse.Password);
            hashPassword = Convert.ToBase64String(provider.ComputeHash(bytes));
            nurse.Password = hashPassword;

            await _unitOfWork.Nurse.Add(nurse);

            return CreatedAtAction("GetNurse", new { id = nurse.Id }, nurse);
        }

        [HttpPost("{login}")]
        public async Task<Nurse> loginNurse(string email, string password)
        {
            return await _unitOfWork.Nurse.login(email, password);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Nurse>> deleteNurse(int id)
        {
            var one = await _unitOfWork.Nurse.Delete(id);
            if (one == null)
            {
                return NotFound();
            }
            return one;
        }
    }
}
