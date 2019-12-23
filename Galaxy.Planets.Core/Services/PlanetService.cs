using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Galaxy.Planets.Core.Interfaces;
using Galaxy.Planets.Core.Models;

namespace Galaxy.Planets.Core.Services
{
    public class PlanetService : IService<Planet>
    {
        private readonly IRepository<Planet> _repository;

        public PlanetService(IRepository<Planet> repository)
        {
            _repository = repository;
        }
        
        public async Task<ActionResponse> AddAsync(Planet model)
        {
            var errors = Validate(model);
            if (errors.Any())
            {
                return new ActionResponse
                {
                    Success = false,
                    Errors = errors
                };
            }

            model.CreatedAt = model.UpdatedAt = DateTime.UtcNow;
            return await _repository.AddAsync(model);
        }

        public async Task<ActionResponse> UpdateAsync(Planet model)
        {
            var errors = Validate(model);
            if (errors.Any())
            {
                return new ActionResponse
                {
                    Success = false,
                    Errors = errors
                };
            }

            model.UpdatedAt = DateTime.UtcNow;
            return await _repository.UpdateAsync(model);
        }

        public async Task<List<Planet>> GetAllAsync()
        {
            var planets = await _repository.GetAsync(null, x => x.OrderByDescending(y => y.UpdatedAt));
            return planets.ToList();
        }

        public Planet GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        private List<ActionError> Validate(Planet model)
        {
            var errors = new List<ActionError>();
            if (string.IsNullOrEmpty(model.Name))
                errors.Add(ActionError.NoName(nameof(Planet)));
                
            if (model.Name.Length > 256)
                errors.Add(ActionError.NameToLong(nameof(Planet)));
         
            return errors;
        }
    }
}