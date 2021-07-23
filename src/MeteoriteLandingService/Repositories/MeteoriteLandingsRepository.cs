namespace Sage.MeteoriteLandingService.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Sage.MeteoriteLandingService.Models;

    public class MeteoriteLandingsRepository : IMeteoriteLandingsRepository
    {
        protected readonly ILogger<MeteoriteLandingsRepository> _logger;
        private readonly datasetsContext _context;

        public MeteoriteLandingsRepository(datasetsContext context, ILogger<MeteoriteLandingsRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Guid?> CreateAsync(MeteoriteLandings meteoriteLanding)
        {
            _logger.LogInformation($"Entering CreateAsync");
            await _context.MeteoriteLandings.AddAsync(meteoriteLanding);
            try {
                var numStateEntriesWritten = await _context.SaveChangesAsync();
                _logger.LogInformation($"Exiting CreateAsync");
                return numStateEntriesWritten > 0 ? meteoriteLanding.Id : null;
            } catch (DbUpdateConcurrencyException dbuce) {
                _logger.LogError(dbuce, $"Failed to create entity with ID {meteoriteLanding.Id} - concurrent access");
                return null;
            } catch (DbUpdateException dbue) {
                _logger.LogError(dbue, $"Failed to create entity with ID {meteoriteLanding.Id}");
                return null;
            }
        }

        public async Task<Guid?> DeleteAsync(MeteoriteLandings meteoriteLanding)
        {
            _logger.LogInformation($"Entering DeleteAsync");
            _context.MeteoriteLandings.Remove(meteoriteLanding);
            try {
                var numStateEntriesWritten = await _context.SaveChangesAsync();
                _logger.LogInformation($"Exiting DeleteAsync");
                return numStateEntriesWritten > 0 ? meteoriteLanding.Id : null;
            } catch (DbUpdateConcurrencyException dbuce) {
                _logger.LogError(dbuce, $"Failed to remove entity with ID {meteoriteLanding.Id} - concurrent access");
                return null;
            } catch (DbUpdateException dbue) {
                _logger.LogError(dbue, $"Failed to remove entity with ID {meteoriteLanding.Id}");
                return null;
            }
        }

        public IAsyncEnumerable<MeteoriteLandings> GetAsync()
        {
            _logger.LogInformation($"Entering GetAsync");
            try {
                // For this example, hardcode a limit of 100 in case there are
                // a lot of records. Generally, you should never be able to 
                // "get all", but rather "get a page" or "get top N"
                var result = _context.MeteoriteLandings.Take(100).AsAsyncEnumerable();
                _logger.LogInformation($"Exiting GetAsync");
                return result;
            } catch(InvalidOperationException ioe) {
                _logger.LogError(ioe, "Take source was null - could not get async enumerable");
                return null;
            } catch (ArgumentNullException ane) {
                _logger.LogError(ane, "Source dataset was not an IAsyncEnumerable - could not get async enumerable");
                return null;
            }
        }

        public async Task<MeteoriteLandings> GetAsync(Guid id)
        {
            _logger.LogInformation($"Entering GetAsync {id}");
            var result = await _context.MeteoriteLandings.FindAsync(id);
            _logger.LogInformation($"Exiting GetAsync {id}");
            return result;
        }

        public async Task<Guid?> UpdateAsync(MeteoriteLandings dbEntity, MeteoriteLandings updateEntity)
        {
            _logger.LogInformation($"Entering UpdateAsync");
            _context.Entry(dbEntity).CurrentValues.SetValues(updateEntity);
            try {
                var numStateEntriesWritten = await _context.SaveChangesAsync();
                _logger.LogInformation($"Exiting UpdateAsync");
                return numStateEntriesWritten > 0 ? updateEntity.Id : null;
            } catch (DbUpdateConcurrencyException dbuce) {
                _logger.LogError(dbuce, $"Failed to update entity with ID {dbEntity.Id} - concurrent access");
                return null;
            } catch (DbUpdateException dbue) {
                _logger.LogError(dbue, $"Failed to update entity with ID {dbEntity.Id}");
                return null;
            }
        }
    }
}
