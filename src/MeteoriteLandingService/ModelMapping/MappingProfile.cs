using AutoMapper;

namespace Sage.MeteoriteLandingService.ModelMapping
{
    using Sage.MeteoriteLandingService.Models;
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Sage.MeteoriteLandingService.ControllerModels.MeteoriteLanding, MeteoriteLandings>();
            CreateMap<MeteoriteLandings, Sage.MeteoriteLandingService.ControllerModels.MeteoriteLanding>();

            CreateMap<Sage.MeteoriteLandingService.ControllerModels.MeteoriteLandingCreateOrUpdate, MeteoriteLandings>();
        }
    }
}
