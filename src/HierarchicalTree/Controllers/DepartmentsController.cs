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
    [Route("api/Departments")]
    public class DepartmentsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public DepartmentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var departments = _unitOfWork.Departments.GetAll();
            return Ok(departments);
        }

        [HttpPost]
        public IActionResult Create(int offeringId, [FromBody] Department department)
        {
            if (department == null)
            {
                return BadRequest();
            }

            var offering = _unitOfWork.Offerings.GetById(offeringId);
            if (department == null)
            {
                return NotFound();
            }

            //validation
            if (offering.Departments.LastOrDefault(x => x.Name == department.Name) != null)
            {
                return BadRequest();
            }

            department.Offering = offering;
            department.OfferingId = offeringId;

            _unitOfWork.Departments.Create(department);
            _unitOfWork.Save();

            return Ok(department);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Department item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            var todo = _unitOfWork.Departments.GetById(id);
            if (todo == null)
            {
                return NotFound();
            }

            _unitOfWork.Departments.Update(item);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var todo = _unitOfWork.Departments.GetById(id);
            if (todo == null)
            {
                return NotFound();
            }

            _unitOfWork.Departments.Delete(id);
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