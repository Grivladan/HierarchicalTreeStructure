using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Interfaces;
using DataAccess.Entities;

namespace WebHost.Controllers
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

        [HttpPost]
        public IActionResult Create([FromBody] Country country)
        {
            if (country == null)
            {
                return BadRequest();
            }

            _unitOfWork.Countries.Create(country);

            return Ok(country);
        }

        [HttpGet]
        public IEnumerable<Country> GetAll()
        {
            var countries = _unitOfWork.Countries.GetAll();
            return countries;
        }
    }
}