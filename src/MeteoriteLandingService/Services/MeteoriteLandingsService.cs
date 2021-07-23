namespace Sage.MeteoriteLandingService.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Sage.MeteoriteLandingService.Models;
    using Sage.MeteoriteLandingService.Repositories;

    public class MeteoriteLandingService : IMeteoriteLandingService
    {
        private readonly ILogger<MeteoriteLandingService> _logger;
        private readonly IMeteoriteLandingsRepository _repository;

        public MeteoriteLandingService(ILogger<MeteoriteLandingService> logger, IMeteoriteLandingsRepository repository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Guid?> CreateAsync(
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
        )
        {
            _logger.LogInformation("Entering Create");
            var entity = new MeteoriteLandings {
                Id = Guid.NewGuid(),
                Name = name,
                MeteoriteId = meteoriteId,
                Nametype = nameType,
                Recclass = recclass,
                Mass = mass,
                Fall = fall,
                Year = year,
                Reclat = reclat,
                Reclong = reclong,
                GeoLocation = geoLocation
            };

            var entityId = await _repository.CreateAsync(entity);
            _logger.LogInformation($"Exiting Create - ID {entityId}");
            return entityId;
        }

        public async Task<(Guid? id, bool entityFound)> DeleteAsync(Guid id)
        {
            _logger.LogInformation($"Entering Delete - ID {id}");
            var dbEntity = await _repository.GetAsync(id);
            if (dbEntity == null) {
                _logger.LogWarning($"Failed to delete meteoriteLanding with ID {id}; MeteoriteLandings not found");
                return (null, false);
            }
            var result = await _repository.DeleteAsync(dbEntity);
            _logger.LogInformation($"Exiting Delete - ID {id}");
            return (result, true);
        }

        public async Task<MeteoriteLandings> GetAsync(Guid id)
        {
            _logger.LogInformation($"Entering GetAsync - ID {id}");
            var entity = await _repository.GetAsync(id);
            _logger.LogInformation($"Exiting GetAsync - ID {id}");
            // entity is null if not found
            return entity;
        }

        public IAsyncEnumerable<MeteoriteLandings> GetAsync()
        {
            _logger.LogInformation("Entering GetAsync");
            var result = _repository.GetAsync();
            _logger.LogInformation("Exiting GetAsync");
            return result;
        }

        public async Task<(Guid? id, bool entityFound)> UpdateAsync(MeteoriteLandings meteoriteLanding)
        {
            _logger.LogInformation($"Entering Update - ID {meteoriteLanding.Id}");
            var dbEntity = await _repository.GetAsync(meteoriteLanding.Id);
            if (dbEntity == null) {
                _logger.LogWarning($"Failed to update meteoriteLanding with ID {meteoriteLanding.Id}; MeteoriteLandings not found");
                return (null, false);
            }

            var result = await _repository.UpdateAsync(dbEntity, meteoriteLanding);    
            _logger.LogInformation($"Exiting Update - ID {meteoriteLanding.Id}");
            return (result, true);
        }
    }
}