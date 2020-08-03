using CityInfo.API.Model;
using CityInfo.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<PointOfInterestsController> _logger;
        private readonly LocalMailService _localMailService;

        // WebHost.CreateDefaultBuilder injects logger into container
        // so we don't have to inject it explicity
        public PointOfInterestsController(ILogger<PointOfInterestsController> logger, LocalMailService localMailService)
        {
            _logger = logger?? throw new ArgumentNullException(nameof(logger));
            _localMailService = localMailService ?? throw new ArgumentNullException(nameof(localMailService));
        }

        [HttpGet]
        public IActionResult GetPointOfInterests(int cityId)
        {
            try
            {
                //throw new Exception();
                var cities = CityDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
                if (cities == null)
                {
                    _logger.LogInformation($"City with id {cityId} was not found while searching point of interests");
                    return NotFound();
                }

                return Ok(cities.PointOfInterests);
            }
            catch (Exception ex)
            {
                // We log the exception as critical & return internal server status code
                _logger.LogCritical($"Exception while getting point of Interests for city id {cityId} ",ex);               
                return StatusCode(500, "A problem happened while handling a request");
            }                
           
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

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdatePointOfInterest(int cityId, int id,PointOfInterestForUpdateDto pointOfInterest)
        {
            var city = CityDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
                return NotFound();

            //if (pointOfInterest == null)
            //    return BadRequest();  // Not required due to ApiController Atrribute.

            if (pointOfInterest.Name == pointOfInterest.Description)
            {
                ModelState.AddModelError("Description", "Description should not be same as Name");
            }

            //This check is not required due to ApiController Attribute, but since we have added custom validation in previous line, let's add it
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pointOfInterestFromStore = city.PointOfInterests.FirstOrDefault(p => p.Id == id);

            if (pointOfInterestFromStore == null)
                return NotFound();

            pointOfInterestFromStore.Name = pointOfInterest.Name;
            pointOfInterestFromStore.Description = pointOfInterest.Description;

            return NoContent();

        }

        [HttpPatch("{id}")]
        public IActionResult PartialUpdatePointOfInterest(int cityId, int id, 
            [FromBody]JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument)
        {
            var city = CityDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
                return NotFound();    

            var pointOfinterestFromStore = city.PointOfInterests.FirstOrDefault(p => p.Id == id);
            if (pointOfinterestFromStore == null)
                return NotFound();

            // Step 1: get pointOfInterestForUpdateDto so that we can apply patch to it. 
            //(patch doc is of type PointOfINterestForUpdateDto)
            var pointOfInterestToPatch = new PointOfInterestForUpdateDto
            {
                Name = pointOfinterestFromStore.Name,
                Description = pointOfinterestFromStore.Description
            };

            // Step 2: apply patch, if there are any errors it will be added to ModelState
            patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);

            // Step 3: verify the patch document is valid or not
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Step 4: validate the updated model
            if (pointOfInterestToPatch.Name == pointOfInterestToPatch.Description)
            {
                ModelState.AddModelError("Description",
                    "Description should not be same as Name");
            }

            if (!TryValidateModel(pointOfInterestToPatch))
                return BadRequest(ModelState);

            // Step 5: update the data in the store
            pointOfinterestFromStore.Name = pointOfInterestToPatch.Name;
            pointOfinterestFromStore.Description = pointOfInterestToPatch.Description;

            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult DeletePointOfInterest(int cityId, int id)
        {
            var city = CityDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
                return NotFound();

            var pointOfInterestFromStore = city.PointOfInterests.FirstOrDefault(p => p.Id == id);
            if (pointOfInterestFromStore == null)
                return NotFound();

            city.PointOfInterests.Remove(pointOfInterestFromStore);

            _localMailService.Send("Point Of Interest deleted",
                $"Point of Interest {pointOfInterestFromStore.Name} with id {pointOfInterestFromStore.Id} was deleted");

            return NoContent();
        }

    }
}
