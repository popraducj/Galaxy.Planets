using System;
using System.Collections.Generic;
using Galaxy.Planets.Core.Enums;
using Galaxy.Teams;

namespace Galaxy.Planets.Presentation.Helpers
{
    public static class TeamExtensions
    {
        public static TeamModel ToTeamModel(this Core.Models.Team team)
        {
            var  response = new TeamModel
            {
                Id = team.Id.ToString(),
                Status = (int) team.Status,
                CaptainId = Guid.Empty.ToString(),
                ShuttleId = Guid.Empty.ToString()
            };
            team.Robots.ForEach(robot => response.RobotsIds.Add(robot.ToString()));
            return response;
        }
        public static Core.Models.Team ToTeam(this TeamModel team)
        {
            if(!Guid.TryParse(team.Id, out _)) return new Core.Models.Team();
            
            var response = new Core.Models.Team
            {
                Id = Guid.Parse(team.Id),
                Status = (TeamStatus) team.Status,
                Robots = new List<Guid>()
            };
            foreach (var robotId in team.RobotsIds)
            {
                response.Robots.Add(Guid.Parse(robotId));
            }

            return response;
        }
    }
}