using CityInfo.API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
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
        [Route("{id}", Name ="GetPointOfInterest")]
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

        [HttpPost]
        public IActionResult CreatePointOfInterest(int cityId, PointOfInterestForCreationDto pointOfInterest)
        {
            var cities = CityDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (cities == null)
                return NotFound();

            //if (pointOfInterest == null)
            //    return BadRequest();  // Not required due to ApiController Atrribute.

            if(pointOfInterest.Name == pointOfInterest.Description)
            {
                ModelState.AddModelError("Description", "Description should not be same as Name");                
            }

            //This check is not required due to ApiController Attribute, but since we have added custom validation in previous line, let's add it
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            int maxPointOfInterestId = CityDataStore.Current
                                        .Cities
                                        .SelectMany(c => c.PointOfInterests).Max(p => p.Id);

            PointOfInterestDto pointOfInterestDto = new PointOfInterestDto
            {
                Id = ++maxPointOfInterestId,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };

            return CreatedAtRoute("GetPointOfInterest", new { cityId, id = pointOfInterestDto.Id },pointOfInterestDto);
        }
    }
}
