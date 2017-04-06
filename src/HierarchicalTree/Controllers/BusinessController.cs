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
    [Route("api/Business")]
    public class BusinessController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public BusinessController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var country = _unitOfWork.Countries.GetById(2);
            var business = new Business { Name = "123", LocationCountryId = 2, LocationCountry = country };
            _unitOfWork.Businesses.Create(business);
            _unitOfWork.Save();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var businesses = _unitOfWork.Businesses.GetAll();
            return Ok(businesses);
        }

        [HttpPost]
        public IActionResult Create(int countryId, [FromBody] Business business)
        {
            if (business == null)
            {
                return BadRequest();
            }

            var country = _unitOfWork.Countries.GetById(countryId);
            if (country == null)
            {
                return NotFound();
            }

            //validation
            if (country.Businesses.LastOrDefault(x => x.Name == business.Name) != null)
            {
                return BadRequest();
            }

            business.LocationCountry = country;
            business.LocationCountryId = countryId;

            _unitOfWork.Businesses.Create(business);
            _unitOfWork.Save();

            return Ok(business);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Business item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            var todo = _unitOfWork.Businesses.GetById(id);
            if (todo == null)
            {
                return NotFound();
            }

            _unitOfWork.Businesses.Update(item);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var todo = _unitOfWork.Businesses.GetById(id);
            if (todo == null)
            {
                return NotFound();
            }

            _unitOfWork.Businesses.Delete(id);
            return new NoContentResult();
        }

        [HttpGet("{id}", Name = "GetBusiness")]
        public IActionResult GetById(int id)
        {
            var item = _unitOfWork.Businesses.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
    }
}