using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("/api/cities/{cityId}/pointOfInterests")]
    public class PointOfInterestsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetPointOfInterests(int cityId)
        {
            var cities = CityDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (cities == null)
                return NotFound();

            return Ok(cities.PointOfInterests);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetPointOfInterests(int cityId, int id)
        {
            var cities = CityDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (cities == null)
                return NotFound();

            var pointOfInterests = cities.PointOfInterests.FirstOrDefault(p => p.Id == id);
            if (pointOfInterests == null)
                return NotFound();

            return Ok(pointOfInterests);
        }
    }
}
