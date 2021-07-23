namespace Sage.MeteoriteLandingService.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    
    using Sage.MeteoriteLandingService.Models;

    public interface IMeteoriteLandingService
    {
        IAsyncEnumerable<MeteoriteLandings> GetAsync();
        Task<MeteoriteLandings> GetAsync(Guid id);
        Task<Guid?> CreateAsync(
            string name, 
            string meteoriteId, 
            string nameType, 
            string recclass, 
            decimal? mass,
            string fall,
            DateTime? year,
            string reclat,
            string reclong,
            string geoLocation
        ); 
        Task<(Guid? id, bool entityFound)> UpdateAsync(MeteoriteLandings meteoriteLanding); 
        Task<(Guid? id, bool entityFound)> DeleteAsync(Guid id); 
    }
}
