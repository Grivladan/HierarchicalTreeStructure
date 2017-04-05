using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HierarchicalTree.Interfaces;
using HierarchicalTree.Entities;
using HierarchicalTree.Models;
using Microsoft.AspNetCore.Identity;

namespace HierarchicalTree.Controllers
{
    [Produces("application/json")]
    [Route("api/Organization")]
    public class OrganizationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
 
        public OrganizationController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Organization organization)
        {
            if (organization == null)
            {
                return BadRequest();
            }

            organization.Owner = await _userManager.GetUserAsync(HttpContext.User);
            organization.OwnerId = organization.Owner.Id;
            _unitOfWork.Organizations.Create(organization);
            _unitOfWork.Save();

            return Ok(organization);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Organization item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            var todo = _unitOfWork.Organizations.GetById(id);
            if (todo == null)
            {
                return NotFound();
            }

            _unitOfWork.Organizations.Update(item);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var todo = _unitOfWork.Organizations.GetById(id);
            if (todo == null)
            {
                return NotFound();
            }

            _unitOfWork.Organizations.Delete(id);
            return new NoContentResult();
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetById(int id)
        {
            var item = _unitOfWork.Organizations.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var organizations = _unitOfWork.Organizations.GetAll();
            return Ok(organizations);
        }
    }
}