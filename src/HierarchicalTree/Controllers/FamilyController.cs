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
    [Route("api/Family")]
    public class FamilyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public FamilyController(IUnitOfWork unitOfWork, ILogger<FamilyController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
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
            _logger.LogInformation(LoggingEvents.CREATE_ITEM, "Create family");
            if (family == null)
            {
                _logger.LogWarning(LoggingEvents.ITEM_IS_NULL, "Passed family is null");
                return BadRequest();
            }

            var business = _unitOfWork.Businesses.GetById(businessId);
            if (business == null)
            {
                _logger.LogWarning(LoggingEvents.GET_ITEM_NOTFOUND, "Business with id {id} doesn't exist", businessId);
                return NotFound();
            }

            //validation
            if (business.Families.LastOrDefault(x => x.Name == family.Name) != null)
            {
                _logger.LogWarning(LoggingEvents.VALIDATION_EXCEPTION, "Family inside each business must be unique");
                return BadRequest();
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
            _logger.LogInformation(LoggingEvents.UPDATE_ITEM, "Update family {id}", id);
            if (item == null)
            {
                _logger.LogWarning(LoggingEvents.ITEM_IS_NULL, "Update family {id}", id);
                return BadRequest();
            }

            var todo = _unitOfWork.Families.GetById(id);
            if (todo == null)
            {
                _logger.LogWarning(LoggingEvents.GET_ITEM_NOTFOUND, "Family with id {id} doesn't exist", id);
                return NotFound();
            }

            _unitOfWork.Families.Update(item);
            _unitOfWork.Save();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation(LoggingEvents.DELETE_ITEM, "Delete family {id}", id);
            var todo = _unitOfWork.Families.GetById(id);
            if (todo == null)
            {
                _logger.LogWarning(LoggingEvents.GET_ITEM_NOTFOUND, "Family with id {id} doesn't exist", id);
                return NotFound();
            }

            _unitOfWork.Families.Delete(id);
            return new NoContentResult();
        }

        //expand business inside tree
        [HttpGet("{businessId}")]
        public IActionResult GetById(int businessId)
        {
            var families = _unitOfWork.Families.Find(x => x.BusinessId == businessId, x => x.Offerings);
            return Ok(families);
        }
    }
}