using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using havis2._0.Models;
using System.Text;
using System.Security.Cryptography;
using havis2._0.UnitOfWorkConfiguration;

namespace havis2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public SecurityController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Security>>> getSecurity()
        {
            return await _unitOfWork.Security.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Security>> getOneSecurity(int id)
        {
            var sec = await _unitOfWork.Security.Get(id);
            if(sec == null)
            {
                return NotFound();
            }
            else
            {
                return sec;
            }
        }

        [HttpPost]
        public async Task<ActionResult<Security>> createSecurity(Security security, string hashPassword)
        {
            var provider = new SHA512CryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(security.Password);
            hashPassword = Convert.ToBase64String(provider.ComputeHash(bytes));
            security.Password = hashPassword;

            await _unitOfWork.Security.Add(security);
            return CreatedAtAction("GetSecurity", new { id = security.Id }, security);
        }

        [HttpPost("{login}")]
        public async Task<Security> loginSecurity(string email, string password)
        {
            return await _unitOfWork.Security.login(email, password);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Security>> updateSecurity(int id, Security security)
        {
            if (id != security.Id)
            {
                return BadRequest();
            }
            else
            {
                await _unitOfWork.Security.Update(security);
                return NoContent();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Security>> deleteSecurity(int id)
        {
            var one = await _unitOfWork.Security.Delete(id);
            if (one == null)
            {
                return NotFound();
            }
            return one;
        }
    }
}
