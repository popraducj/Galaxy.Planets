using System;
using System.Threading.Tasks;
using Galaxy.Planets.Core.Interfaces;
using Galaxy.Planets.Core.Models;
using Galaxy.Planets.Presentation.Helpers;
using Galaxy.Teams;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using Team = Galaxy.Teams.Team;

namespace Galaxy.Planets.Presentation.Services.Teams
{
    public class TeamService : IGrpcService<Core.Models.Team>
    {
        private readonly ILogger<TeamService> _logger;
        private readonly Team.TeamClient _client;

        public TeamService(ILogger<TeamService> logger)
        {
            _logger = logger;
            var channel = GrpcChannel.ForAddress("https://localhost:5005");
            _client = new Team.TeamClient(channel);
        }
        public async Task<Core.Models.Team> GetByIdAsync(Guid id)
        {
            var teamModel = await _client.GetByIdAsync(new IdRequest
            {
                Id = id.ToString()
            });
            return teamModel.ToTeam();
        }

        public async Task<ActionResponse> UpdateStatusAsync(Core.Models.Team team)
        {
            var result = await _client.UpdateStatusAsync(team.ToTeamModel());
            return result.ToActionResponse();
        }
    }
}