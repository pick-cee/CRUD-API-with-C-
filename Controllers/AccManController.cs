using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using havis2._0.Models;
using System.Security.Cryptography;
using System.Text;
using havis2._0.UnitOfWorkConfiguration;

namespace havis2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccManController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccManController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccMan>>> getAccMan()
        {
            return await _unitOfWork.AccMan.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccMan>> getOne(int id)
        {
            var one = await _unitOfWork.AccMan.Get(id);
            if (one == null)
            {
                return NotFound();
            }
            return one;
        }

        [HttpPost]
        public async Task<ActionResult<AccMan>> createAccMan(AccMan accMan, string hashPassword)
        {
            var provider = new SHA512CryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(accMan.Password);
            hashPassword = Convert.ToBase64String(provider.ComputeHash(bytes));
            accMan.Password = hashPassword;

            await _unitOfWork.AccMan.Add(accMan);

            return CreatedAtAction("GetAccMan", new { id = accMan.Id }, accMan);
        }

        [HttpPost("{login}")]
        public async Task<AccMan> login(string email, string password)
        {
            return await _unitOfWork.AccMan.login(email, password);
        }



        [HttpPut("{id}")]
        public async Task<ActionResult<AccMan>> updateAccMan(int id, AccMan accMan)
        {
            if (id != accMan.Id)
            {
                return BadRequest();
            }
            else
            {
                await _unitOfWork.AccMan.Update(accMan);
                return NoContent();
            }
        }
        [HttpDelete]
        public async Task<ActionResult<AccMan>> deleteAccMan(int id)
        {
            var one = await _unitOfWork.AccMan.Delete(id);
            if(one == null)
            {
                return NotFound();
            }
            return one;
        }
    }
}
