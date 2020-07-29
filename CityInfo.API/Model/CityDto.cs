using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Model
{
    public class CityDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<PointOfInterestDto> PointOfInterests { get; set; } = new List<PointOfInterestDto>();

        public int NumberOfPointsOfInterest
        {
            get { return PointOfInterests.Count; }
        }

    }
}
