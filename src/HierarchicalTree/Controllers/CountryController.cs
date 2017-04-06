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
    [Route("api/Country")]
    public class CountryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        public CountryController(IUnitOfWork unitOfWork, ILogger<CountryController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var countries = _unitOfWork.Countries.GetAll(x => x.Businesses);
            return Ok(countries);
        }

        [HttpPost]
        public IActionResult Create(int organizationId, [FromBody] Country country)
        {
            _logger.LogInformation(LoggingEvents.CREATE_ITEM, "Create country");
            if (country == null)
            {
                _logger.LogWarning(LoggingEvents.ITEM_IS_NULL, "Passed country is null");
                return BadRequest();
            }

            var organization = _unitOfWork.Organizations.GetById(organizationId);
            if (organization == null)
            {
                _logger.LogWarning(LoggingEvents.GET_ITEM_NOTFOUND, "Organization with id {id} doesn't exist", organizationId);
                return NotFound();
            }
         
            //validation
            if(organization.Countries.LastOrDefault(x => x.Name == country.Name) != null)
            {
                _logger.LogWarning(LoggingEvents.VALIDATION_EXCEPTION, "Country inside each organization must be unique");
                return BadRequest();
            }
                           
            country.Organization = organization;
            country.OrganizationId = organizationId;

            _unitOfWork.Countries.Create(country);
            _unitOfWork.Save();

            return Ok(organization);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Country item)
        {
            _logger.LogInformation(LoggingEvents.UPDATE_ITEM, "Update country {id}", id);
            if (item == null)
            {
                _logger.LogWarning(LoggingEvents.ITEM_IS_NULL, "Update country {id}", id);
                return BadRequest();
            }

            var todo = _unitOfWork.Countries.GetById(id);
            if (todo == null)
            {
                _logger.LogWarning(LoggingEvents.GET_ITEM_NOTFOUND, "Country with id {id} doesn't exist", id);
                return NotFound();
            }

            _unitOfWork.Countries.Update(item);
            _unitOfWork.Save();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation(LoggingEvents.DELETE_ITEM, "Delete country {id}", id);
            var todo = _unitOfWork.Countries.GetById(id);
            if (todo == null)
            {
                _logger.LogWarning(LoggingEvents.GET_ITEM_NOTFOUND, "Country with id {id} doesn't exist", id);
                return NotFound();
            }

            _unitOfWork.Countries.Delete(id);
            _unitOfWork.Save();
            return new NoContentResult();
        }

        //expand organization, get countries inside organization
        [HttpGet("{organizationId}", Name = "GetCountries")]
        public IActionResult GetCountries(int organizationId)
        {
            var countries = _unitOfWork.Countries.Find(x => x.OrganizationId == organizationId, x => x.Businesses);
            return Ok(countries);
        }
    }
}