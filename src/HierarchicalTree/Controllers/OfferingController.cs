using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HierarchicalTree.Interfaces;
using HierarchicalTree.Entities;
using Microsoft.Extensions.Logging;

namespace HierarchicalTree.Controllers
{
    [Produces("application/json")]
    [Route("api/Offering")]
    public class OfferingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public OfferingController(IUnitOfWork unitOfWork, ILogger<OfferingController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
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
            _logger.LogInformation(LoggingEvents.CREATE_ITEM, "Create offering");
            if (offering == null)
            {
                _logger.LogWarning(LoggingEvents.ITEM_IS_NULL, "Passed offering is null");
                return BadRequest();
            }

            var family = _unitOfWork.Families.GetById(familyId);
            if (family == null)
            {
                _logger.LogWarning(LoggingEvents.GET_ITEM_NOTFOUND, "Family with id {id} doesn't exist", familyId);
                return NotFound();
            }

            //validation
            if (family.Offerings.LastOrDefault(x => x.Name == offering.Name) != null)
            {
                _logger.LogWarning(LoggingEvents.VALIDATION_EXCEPTION, "Offering inside each family must be unique");
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
            _logger.LogInformation(LoggingEvents.UPDATE_ITEM, "Update offering {id}", id);
            if (item == null)
            {
                _logger.LogWarning(LoggingEvents.ITEM_IS_NULL, "Update offering {id}", id);
                return BadRequest();
            }

            var todo = _unitOfWork.Offerings.GetById(id);
            if (todo == null)
            {
                _logger.LogWarning(LoggingEvents.GET_ITEM_NOTFOUND, "Offering with id {id} doesn't exist", id);
                return NotFound();
            }

            _unitOfWork.Offerings.Update(item);
            _unitOfWork.Save();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation(LoggingEvents.DELETE_ITEM, "Delete offering {id}", id);
            var todo = _unitOfWork.Offerings.GetById(id);
            if (todo == null)
            {
                _logger.LogWarning(LoggingEvents.GET_ITEM_NOTFOUND, "Offering with id {id} doesn't exist", id);
                return NotFound();
            }

            _unitOfWork.Offerings.Delete(id);
            _unitOfWork.Save();
            return new NoContentResult();
        }

        //add expand for families
        [HttpGet("{familyId}", Name = "GetOfferings")]
        public IActionResult GetById(int familyId)
        {
            var offerings = _unitOfWork.Offerings.Find( x => x.FamilyId == familyId, x => x.Departments);
            return new ObjectResult(offerings);
        }
    }
}