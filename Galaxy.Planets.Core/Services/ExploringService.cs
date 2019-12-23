using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Galaxy.Planets.Core.Enums;
using Galaxy.Planets.Core.Interfaces;
using Galaxy.Planets.Core.Models;

namespace Galaxy.Planets.Core.Services
{
    public class ExploringService : IService<Exploration>
    {
        private readonly IRepository<Exploration> _repository;
        private readonly IService<Planet> _planetService;
        private readonly IGrpcService<Team> _teamService;
        private readonly IGrpcService<Robot> _robotService;

        public ExploringService(IRepository<Exploration> repository, IService<Planet> planetService, IGrpcService<Team> teamService, IGrpcService<Robot> robotService)
        {
            _repository = repository;
            _planetService = planetService;
            _teamService = teamService;
            _robotService = robotService;
        }
        public async Task<ActionResponse> AddAsync(Exploration model)
        {
            var planet = _planetService.GetById(model.PlanetId);
            if (planet == null) return ActionResponse.NotFound(nameof(Planet));

            var team = await _teamService.GetByIdAsync(model.TeamId);
            if (team.Id == Guid.Empty) return ActionResponse.NotFound(nameof(Team));
            if (team.Status != TeamStatus.Ready)
                return new ActionResponse
                {
                    Success = false,
                    Errors = new List<ActionError>
                    {
                        new ActionError
                        {
                            Code = "TeamNotReady",
                            Description = "The team assigned should be in ready state"
                        }
                    }
                };

            team.Status = TeamStatus.InTransit;
            await _teamService.UpdateStatusAsync(team);
            
            // make sure you have the correct data
            model.Status = ExplorationStatus.EnRouteToPlanet;
           
            // simulate a complex calculation between the shuttle speed, distance between planets
            var transportDuration = new Random().Next(1, 30);
            model.PhaseFinishTime = DateTime.Now.AddSeconds(transportDuration);
            model.CreatedAt = model.UpdatedAt = DateTime.UtcNow;
            
            return await _repository.AddAsync(model);
        }

        public async Task<ActionResponse> UpdateAsync(Exploration model)
        {
            switch (model.Status)
            {
                case ExplorationStatus.EnRouteToPlanet:
                    return ActionResponse.NotFound(nameof(Exploration));
                case ExplorationStatus.Exploring:
                {
                    // simulate a complex calculation between robots units covered, planet units, and robot speed
                    var explorationTime = new Random().Next(1, 30);
                    model.PhaseFinishTime = DateTime.Now.AddSeconds(explorationTime);
                    var team = await _teamService.GetByIdAsync(model.TeamId);
                    foreach (var robotId in team.Robots)
                    {
                        var robot = await _robotService.GetByIdAsync(robotId);
                        robot.Status = RobotStatus.Exploring;
                        await _robotService.UpdateStatusAsync(robot);
                    }

                    break;
                }
                case ExplorationStatus.Finished:
                {
                    var team = await _teamService.GetByIdAsync(model.TeamId);
                    foreach (var robotId in team.Robots)
                    {
                        var robot = await _robotService.GetByIdAsync(robotId);
                        //simulate result 
                        var isOk = new Random().Next(1, 100) % 2  + 1;
                        model.RobotsReports.Add((ExplorationResultStatus)(isOk));
                    }

                    team.Status = TeamStatus.Ready;
                    await _teamService.UpdateStatusAsync(team);
                    // here send alert to captain that the mission has finished
                    break;
                }
            }


            model.UpdatedAt = DateTime.UtcNow;
            return await _repository.UpdateAsync(model);
        }

        public async Task<List<Exploration>> GetAllAsync()
        {
            var explorations = await _repository.GetAsync(null, x => x.OrderByDescending(y => y.UpdatedAt));
            return explorations.ToList();
        }

        public Exploration GetById(Guid id)
        {
            return _repository.GetById(id);
        }
    }
}