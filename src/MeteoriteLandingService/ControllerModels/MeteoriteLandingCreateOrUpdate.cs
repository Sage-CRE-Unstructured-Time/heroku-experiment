using System;

namespace Sage.MeteoriteLandingService.ControllerModels
{
    public class MeteoriteLandingCreateOrUpdate
    {
        public string Name { get; set; }
        public string MeteoriteId { get; set; }
        public string Nametype { get; set; }
        public string Recclass { get; set; }
        public decimal? Mass { get; set; }
        public string Fall { get; set; }
        public DateTime? Year { get; set; }
        public string Reclat { get; set; }
        public string Reclong { get; set; }
        public string GeoLocation { get; set; }
    }
}
