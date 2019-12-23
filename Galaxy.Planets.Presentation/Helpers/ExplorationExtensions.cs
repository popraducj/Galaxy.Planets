using System;
using System.Collections.Generic;
using Galaxy.Planets.Core.Enums;

namespace Galaxy.Planets.Presentation.Helpers
{
    public static class ExplorationExtensions
    {
        public static ExplorationModel ToExplorationModel(this Core.Models.Exploration exploration)
        {
            if (exploration == null) return new ExplorationModel();
            
            var result = new ExplorationModel
            {
                Id = exploration.Id.ToString(),
                Name = exploration.Name?? string.Empty,
                Status = (int) exploration.Status,
                PlanetId = exploration.PlanetId.ToString(),
                TeamId = exploration.TeamId.ToString()
            };
            
            exploration.RobotsReports.ForEach(status => result.RobotsReports.Add((int)status));

            return result;
        }

        public static Core.Models.Exploration ToExploration(this ExplorationModel model)
        {
            var result = new Core.Models.Exploration
            {
                Id = Guid.Parse(model.Id),
                Name = model.Name,
                Status = (ExplorationStatus) model.Status,
                PlanetId = Guid.Parse(model.PlanetId),
                TeamId = Guid.Parse(model.TeamId),
                RobotsReports = new List<ExplorationResultStatus>()
            };

            foreach (var report in model.RobotsReports)
            {
                result.RobotsReports.Add((ExplorationResultStatus) report);
            }

            return result;
        }
    }
}