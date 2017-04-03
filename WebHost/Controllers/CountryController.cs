using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Entities;

namespace WebHost.Controllers
{

    [Produces("application/json")]
    [Route("api/Country")]
    public class CountryController : Controller
    {
      [HttpGet]
      public IActionResult GetCountry()
      {
            Country country = new Country();
            country.Name = "Ukraine";
            return new ObjectResult(country);
      }
    }
}