using System;
using System.Threading.Tasks;
using Galaxy.Planets.Core.Interfaces;
using Galaxy.Planets.Core.Models;
using Galaxy.Planets.Presentation.Helpers;
using Galaxy.Teams;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using Robot = Galaxy.Teams.Robot;

namespace Galaxy.Planets.Presentation.Services.Teams
{
    public class RobotService : IGrpcService<Core.Models.Robot>
    {
        private readonly ILogger<RobotService> _logger;
        private readonly Robot.RobotClient _client;

        public RobotService(ILogger<RobotService> logger)
        {
            _logger = logger;
            var channel = GrpcChannel.ForAddress("https://localhost:5005");
            _client = new Robot.RobotClient(channel);
        }
        public async Task<Core.Models.Robot> GetByIdAsync(Guid id)
        {
            var robotModel = await _client.GetByIdAsync(new IdRequest
            {
                Id = id.ToString()
            });
            return robotModel.ToRobot();
        }

        public async Task<ActionResponse> UpdateStatusAsync(Core.Models.Robot robot)
        {
            var result = await _client.UpdateStatusAsync(robot.ToRobotModel());
            return result.ToActionResponse();
        }
    }
}