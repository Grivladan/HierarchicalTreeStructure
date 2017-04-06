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
    [Route("api/Business")]
    public class BusinessController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public BusinessController(IUnitOfWork unitOfWork, ILogger<BusinessController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
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
            _logger.LogInformation(LoggingEvents.CREATE_ITEM, "Create business");
            if (business == null)
            {
                _logger.LogWarning(LoggingEvents.ITEM_IS_NULL, "Passed business is null");
                return BadRequest();
            }

            var country = _unitOfWork.Countries.GetById(countryId);
            if (country == null)
            {
                _logger.LogWarning(LoggingEvents.GET_ITEM_NOTFOUND, "Country with id {id} doesn't exist", countryId);
                return NotFound();
            }

            //validation
            if (country.Businesses.LastOrDefault(x => x.Name == business.Name) != null)
            {
                _logger.LogWarning(LoggingEvents.VALIDATION_EXCEPTION, "Business inside each country must be unique");
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
            _logger.LogInformation(LoggingEvents.UPDATE_ITEM, "Update business {id}", id);
            if (item == null)
            {
                _logger.LogWarning(LoggingEvents.ITEM_IS_NULL, "Update business {id}", id);
                return BadRequest();
            }

            var todo = _unitOfWork.Businesses.GetById(id);
            if (todo == null)
            {
                _logger.LogWarning(LoggingEvents.GET_ITEM_NOTFOUND, "Business with id {id} doesn't exist", id);
                return NotFound();
            }

            _unitOfWork.Businesses.Update(item);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation(LoggingEvents.DELETE_ITEM, "Delete business {id}", id);
            var todo = _unitOfWork.Businesses.GetById(id);
            if (todo == null)
            {
                _logger.LogWarning(LoggingEvents.GET_ITEM_NOTFOUND, "Business with id {id} doesn't exist", id);
                return NotFound();
            }

            _unitOfWork.Businesses.Delete(id);
            return new NoContentResult();
        }

        //expand business inside country
        [HttpGet("{countryId}", Name = "GetBusiness")]
        public IActionResult GetById(int countryId)
        {
            var businesses = _unitOfWork.Businesses.Find(x => x.LocationCountryId == countryId, x => x.Families);
            return Ok(businesses);
        }
    }
}