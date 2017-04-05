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
    [Route("api/Country")]
    public class CountryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CountryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var countries = _unitOfWork.Countries.GetAll();
            return Ok(countries);
        }

        [HttpPost]
        public IActionResult Create(int organizationId, [FromBody] Country country)
        {
            if (country == null)
            {
                return BadRequest();
            }

            var organization = _unitOfWork.Organizations.GetById(organizationId);
            if (organization == null)
            {
                return NotFound();
            }
         
            //validation
            if(organization.Countries.LastOrDefault(x => x.Name == country.Name) != null)
            {
                return BadRequest();
            }
                           
            country.Organization = organization;

            _unitOfWork.Countries.Create(country);
            _unitOfWork.Save();

            return Ok(organization);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Country item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            var todo = _unitOfWork.Countries.GetById(id);
            if (todo == null)
            {
                return NotFound();
            }

            _unitOfWork.Countries.Update(item);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var todo = _unitOfWork.Countries.GetById(id);
            if (todo == null)
            {
                return NotFound();
            }

            _unitOfWork.Countries.Delete(id);
            return new NoContentResult();
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetById(int id)
        {
            var item = _unitOfWork.Countries.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
    }
}