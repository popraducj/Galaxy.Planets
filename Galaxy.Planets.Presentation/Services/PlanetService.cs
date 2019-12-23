using System;
using System.Threading.Tasks;
using Galaxy.Planets.Core.Interfaces;
using Galaxy.Planets.Presentation.Helpers;
using Galaxy.Teams;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Galaxy.Planets.Presentation.Services
{
    public class PlanetService: Planet.PlanetBase
    {
        private readonly ILogger<PlanetService> _logger;
        private readonly IService<Core.Models.Planet> _planetService;

        public PlanetService(ILogger<PlanetService> logger, IService<Core.Models.Planet> planetService)
        {
            _logger = logger;
            _planetService = planetService;
        }

        public override async Task<ActionReplay> Add(PlanetModel planetModel, ServerCallContext context)
        {
            var result = await _planetService.AddAsync(planetModel.ToPlanet());

            return  result.ToActionReplay();
        }

        public override async Task<ActionReplay> Update(PlanetModel planetModel, ServerCallContext context)
        {
            var result = await _planetService.UpdateAsync(planetModel.ToPlanet());
            return result.ToActionReplay();
        }

        public override async Task GetAll(Empty request, IServerStreamWriter<PlanetModel> responseStream, ServerCallContext context)
        {
            var results = await _planetService.GetAllAsync();
            results.ForEach(async planet => { await responseStream.WriteAsync(planet.ToPlanetModel()); });
        }

        public override Task<PlanetModel> GetById(IdRequest request, ServerCallContext context)
        {
            var planet = _planetService.GetById(Guid.Parse(request.Id));
            return Task.FromResult(planet.ToPlanetModel());
        }
    }
}