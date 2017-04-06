using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HierarchicalTree.Interfaces;
using HierarchicalTree.Entities;
using HierarchicalTree.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace HierarchicalTree.Controllers
{
    [Produces("application/json")]
    [Route("api/Organization")]
    public class OrganizationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;
 
        public OrganizationController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager,
            ILogger<OrganizationController> logger)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _logger = logger;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Organization organization)
        {
                _logger.LogInformation(LoggingEvents.CREATE_ITEM, "Create organization");
                if (organization == null)
                {
                    _logger.LogWarning(LoggingEvents.ITEM_IS_NULL, "Passed organization is null");
                    return BadRequest();
                }

                organization.Owner = await _userManager.GetUserAsync(HttpContext.User);
                organization.OwnerId = organization.Owner.Id;
                _unitOfWork.Organizations.Create(organization);
                _unitOfWork.Save();

                return Ok(organization);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Organization item)
        {
            _logger.LogInformation(LoggingEvents.UPDATE_ITEM, "Update organization {id}", id);
            if (item == null)
            {
                _logger.LogWarning(LoggingEvents.ITEM_IS_NULL, "Update organization {id}", id);
                return BadRequest();
            }

            var todo = _unitOfWork.Organizations.GetById(id);
            if (todo == null)
            {
                _logger.LogWarning(LoggingEvents.GET_ITEM_NOTFOUND, "Organization with id {id} doesn't exist", id);
                return NotFound();
            }

            _unitOfWork.Organizations.Update(item);
            _unitOfWork.Save();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation(LoggingEvents.DELETE_ITEM, "Delete organization {id}", id);
            var organization = _unitOfWork.Organizations.GetById(id);
            if (organization == null)
            {
                _logger.LogWarning(LoggingEvents.GET_ITEM_NOTFOUND, "Organization with id {id} doesn't exist", id);
                return NotFound();
            }

            _unitOfWork.Organizations.Delete(id);
            _unitOfWork.Save();
            return new NoContentResult();
        }

        [HttpGet("{id}", Name = "GetOrganization")]
        public IActionResult GetById(int id)
        {
            var organization = _unitOfWork.Organizations.GetById(id);
            if (organization == null)
            {
                _logger.LogWarning(LoggingEvents.GET_ITEM_NOTFOUND, "Organization with id {id} doesn't exist", id);
                return NotFound();
            }
            return new ObjectResult(organization);
        }

        //tree upper level, get list of organization of current user
        [HttpGet(Name = "GetTree")]
        public IActionResult GetTree()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var organizations = _unitOfWork.Organizations.Find( x => x.OwnerId == userId, x=>x.Countries);
 
            return Ok(organizations);
        }
    }
}