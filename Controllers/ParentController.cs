using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using havis2._0.Models;
using System.Security.Cryptography;
using System.Text;
using havis2._0.UnitOfWorkConfiguration;

namespace havis2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ParentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Parent>>> getParent()
        {
            return await _unitOfWork.Parent.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Parent>> getOne(int id)
        {
            var one = await _unitOfWork.Parent.Get(id);
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
        public async Task<ActionResult<Parent>> createParent(Parent parent, string hashPassword)
        {
            var provider = new SHA512CryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(parent.Password);
            hashPassword = Convert.ToBase64String(provider.ComputeHash(bytes));
            parent.Password = hashPassword;

            await _unitOfWork.Parent.Add(parent);
            return CreatedAtAction("GetParent", new { id = parent.Id }, parent);
        }

        [HttpPost("{login}")]
        public async Task<Parent> loginParent(string email, string password)
        {
            return await _unitOfWork.Parent.login(email, password);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Parent>> updateParent(int id, Parent parent)
        {
            if (id != parent.Id)
            {
                return BadRequest();
            }
            else
            {
                await _unitOfWork.Parent.Update(parent);
                return NoContent();
            }
        }
        [HttpDelete]
        public async Task<ActionResult<Parent>> deleteParent(int id)
        {
            var one = await _unitOfWork.Parent.Delete(id);
            if (one == null)
            {
                return NotFound();
            }
            return one;
        }
    }
}
