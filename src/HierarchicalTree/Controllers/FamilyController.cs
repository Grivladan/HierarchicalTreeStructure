using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HierarchicalTree.Interfaces;
using HierarchicalTree.Entities;

namespace HierarchicalTree.Controllers
{
    [Produces("application/json")]
    [Route("api/Family")]
    public class FamilyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public FamilyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var families = _unitOfWork.Families.GetAll();
            return Ok(families);
        }

        [HttpPost]
        public IActionResult Create(int businessId, [FromBody] Family family)
        {
            if (family == null)
            {
                return BadRequest();
            }

            var business = _unitOfWork.Businesses.GetById(businessId);
            if (business == null)
            {
                return NotFound();
            }
            family.Business = business;
            family.BusinessId = businessId;

            _unitOfWork.Families.Create(family);
            _unitOfWork.Save();

            return Ok(family);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Family item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            var todo = _unitOfWork.Families.GetById(id);
            if (todo == null)
            {
                return NotFound();
            }

            _unitOfWork.Families.Update(item);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var todo = _unitOfWork.Families.GetById(id);
            if (todo == null)
            {
                return NotFound();
            }

            _unitOfWork.Families.Delete(id);
            return new NoContentResult();
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetById(int id)
        {
            var item = _unitOfWork.Families.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
    }
}