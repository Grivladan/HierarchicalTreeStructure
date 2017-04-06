using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HierarchicalTree.Interfaces;
using HierarchicalTree.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace HierarchicalTree.Controllers
{
    [Produces("application/json")]
    [Route("api/Departments")]
    public class DepartmentsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public DepartmentsController(IUnitOfWork unitOfWork, ILogger<OfferingController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var departments = _unitOfWork.Departments.GetAll();
            return Ok(departments);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(int offeringId, [FromBody] Department department)
        {
            _logger.LogInformation(LoggingEvents.CREATE_ITEM, "Create department");
            if (department == null)
            {
                _logger.LogWarning(LoggingEvents.ITEM_IS_NULL, "Passed department is null");
                return BadRequest();
            }

            var offering = _unitOfWork.Offerings.GetById(offeringId);
            if (department == null)
            {
                _logger.LogWarning(LoggingEvents.GET_ITEM_NOTFOUND, "Offering with id {id} doesn't exist", offeringId);
                return NotFound();
            }

            //validation
            if (offering.Departments.LastOrDefault(x => x.Name == department.Name) != null)
            {
                _logger.LogWarning(LoggingEvents.VALIDATION_EXCEPTION, "Department inside each offering must be unique");
                return BadRequest();
            }

            department.Offering = offering;
            department.OfferingId = offeringId;

            _unitOfWork.Departments.Create(department);
            _unitOfWork.Save();

            return Ok(department);
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Update(int id, [FromBody] Department item)
        {
            _logger.LogInformation(LoggingEvents.UPDATE_ITEM, "Update department {id}", id);
            if (item == null)
            {
                _logger.LogWarning(LoggingEvents.ITEM_IS_NULL, "Update department {id}", id);
                return BadRequest();
            }

            var todo = _unitOfWork.Departments.GetById(id);
            if (todo == null)
            {
                _logger.LogWarning(LoggingEvents.GET_ITEM_NOTFOUND, "Department with id {id} doesn't exist", id);
                return NotFound();
            }

            _unitOfWork.Departments.Update(item);
            _unitOfWork.Save();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation(LoggingEvents.DELETE_ITEM, "Delete department {id}", id);
            var todo = _unitOfWork.Departments.GetById(id);
            if (todo == null)
            {
                _logger.LogWarning(LoggingEvents.GET_ITEM_NOTFOUND, "Department with id {id} doesn't exist", id);
                return NotFound();
            }

            _unitOfWork.Departments.Delete(id);
            _unitOfWork.Save();
            return new NoContentResult();
        }

        [HttpGet("{id}", Name = "GetDepartment")]
        public IActionResult GetById(int id)
        {
            var item = _unitOfWork.Departments.GetById(id);
            return new ObjectResult(item);
        }
    }
}