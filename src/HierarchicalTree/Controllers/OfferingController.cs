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
    [Route("api/Offering")]
    public class OfferingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public OfferingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var offerings = _unitOfWork.Offerings.GetAll();
            return Ok(offerings);
        }

        [HttpPost]
        public IActionResult Create(int familyId, [FromBody] Offering offering)
        {
            if (offering == null)
            {
                return BadRequest();
            }

            var family = _unitOfWork.Families.GetById(familyId);
            if (family == null)
            {
                return NotFound();
            }

            //validation
            if (family.Offerings.LastOrDefault(x => x.Name == offering.Name) != null)
            {
                return BadRequest();
            }

            offering.Family = family;
            offering.FamilyId = familyId;

            _unitOfWork.Offerings.Create(offering);
            _unitOfWork.Save();

            return Ok(family);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Offering item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            var todo = _unitOfWork.Offerings.GetById(id);
            if (todo == null)
            {
                return NotFound();
            }

            _unitOfWork.Offerings.Update(item);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var todo = _unitOfWork.Offerings.GetById(id);
            if (todo == null)
            {
                return NotFound();
            }

            _unitOfWork.Offerings.Delete(id);
            return new NoContentResult();
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetById(int id)
        {
            var item = _unitOfWork.Offerings.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
    }
}