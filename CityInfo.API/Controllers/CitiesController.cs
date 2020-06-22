using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Controllers
{
    [ApiController] // This improves api development experience
    // If this is not applied, and route is not configured, we will get 404 error
    // when we apply the attribute, and route is not configured, it will say route not configured

    //[Route("api/[Controller]")] // third way of routing
    [Route("api/cities")] // Preffered way of routing 
    public class CitiesController: ControllerBase
    {
        //[HttpGet("api/cities")] This is one way of routing
        //[Route("api/cities")] second way of routing

        [HttpGet]
        public JsonResult GetCities()
        {
            return new JsonResult(new List<object>
            {
                new {id=1, Name="Pune"},
                new {id=2, Name="Mumbai"}
            });
        }
    }
}
