namespace Sage.MeteoriteLandingService.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Sage.MeteoriteLandingService.Models;
    
    public interface IMeteoriteLandingsRepository
    {
        Task<MeteoriteLandings> GetAsync(Guid Id);
        IAsyncEnumerable<MeteoriteLandings> GetAsync(); 
        Task<Guid?> CreateAsync(MeteoriteLandings meteoriteLanding); 
        Task<Guid?> UpdateAsync(MeteoriteLandings dbEntity, MeteoriteLandings updateEntity); 
        Task<Guid?> DeleteAsync(MeteoriteLandings meteoriteLanding); 
    }
}
