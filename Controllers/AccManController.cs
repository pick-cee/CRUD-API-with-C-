using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using havis2._0.Models;
using System.Security.Cryptography;
using System.Text;
using havis2._0.Repository;
using havis2._0.Repository.AccManRepo;

namespace havis2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccManController : Controller
    {
        //private readonly havisContext _context;
        private readonly IRepository<AccMan> _repository;
        private readonly IAccManRepo _accManRepo;

        public AccManController(IAccManRepo accManRepo)
        {
            _accManRepo = accManRepo;
        }

        public AccManController(IRepository<AccMan> repository)
        {
            _repository = repository;
        }

        //public AccManController(havisContext context)
        //{
        //    _context = context;
        //}

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccMan>>> getAccMan()
        {
            return await _repository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccMan>> getOne(int id)
        {
            var one = await _repository.Get(id);
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

            await _repository.Add(accMan);

            return CreatedAtAction("GetAccMan", new { id = accMan.Id }, accMan);
        }

        [HttpPost("{login}")]
        public async Task<AccMan> login(string email, string password)
        {
            return await _accManRepo.login(email, password);
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
                await _repository.Update(accMan);
                return NoContent();
            }
        }
        [HttpDelete]
        public async Task<ActionResult<AccMan>> deleteAccMan(int id)
        {
            var one = await _repository.Delete(id);
            if(one == null)
            {
                return NotFound();
            }
            return one;
        }
    }
}
