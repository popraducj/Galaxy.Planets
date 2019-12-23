using System;
using System.Threading.Tasks;
using Galaxy.Planets.Core.Interfaces;
using Galaxy.Planets.Core.Models;
using Galaxy.Planets.Presentation.Helpers;
using Galaxy.Teams;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Galaxy.Planets.Presentation.Services
{
    public class ExplorationService: Exploration.ExplorationBase
    {
        private readonly ILogger<ExplorationService> _logger;
        private readonly IService<Core.Models.Exploration> _explorationService;

        public ExplorationService(ILogger<ExplorationService> logger, IService<Core.Models.Exploration> explorationService)
        {
            _logger = logger;
            _explorationService = explorationService;
        }

        public override async Task<ActionReplay> Add(ExplorationModel explorationModel, ServerCallContext context)
        {
            var result = await _explorationService.AddAsync(explorationModel.ToExploration());

            return  result.ToActionReplay();
        }
        
        public override async Task GetAll(Empty request, IServerStreamWriter<ExplorationModel> responseStream, ServerCallContext context)
        {
            var results = await _explorationService.GetAllAsync();
            results.ForEach(async exploration => { await responseStream.WriteAsync(exploration.ToExplorationModel()); });
        }

        public override Task<ExplorationModel> GetById(IdRequest request, ServerCallContext context)
        {
            var exploration = _explorationService.GetById(Guid.Parse(request.Id));
            return Task.FromResult(exploration.ToExplorationModel());
        }
    }
}