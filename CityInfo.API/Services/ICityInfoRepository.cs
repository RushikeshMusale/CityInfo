using CityInfo.API.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Services
{
    public interface ICityInfoRepository
    {
        IEnumerable<City> GetCities();

        City GetCity(int id, bool includePointOfInterests);

        IEnumerable<PointOfInterest> GetPointOfInterestsForACity(int cityId);

        PointOfInterest GetPointOfInterestForACity(int cityId, int pointOfInterestId);

        bool CityExists(int cityId);
        void AddPointOfInterest(int cityId, PointOfInterest pointOfInterestfinal);
        bool Save();
    }
}
