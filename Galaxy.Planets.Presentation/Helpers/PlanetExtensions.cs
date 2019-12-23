using System;
using Galaxy.Planets.Core.Enums;

namespace Galaxy.Planets.Presentation.Helpers
{
    public static class PlanetExtensions
    {
        public static PlanetModel ToPlanetModel(this Core.Models.Planet planet)
        {
            if (planet == null) return new PlanetModel();
            return new PlanetModel()
            {
                Id = planet.Id.ToString(),
                Description = planet.Description ?? string.Empty,
                Name = planet.Name ?? string.Empty,
                Status = (int) planet.Status,
                Units = planet.Units,
                ImagePath = planet.ImagePath ?? string.Empty
            };
        }

        public static Core.Models.Planet ToPlanet(this PlanetModel planetModel)
        {
            return new Core.Models.Planet
            {
                Id = Guid.Parse(planetModel.Id),
                Description = planetModel.Description,
                Name = planetModel.Name,
                Status = (PlanetStatus) planetModel.Status,
                Units = planetModel.Units,
                ImagePath = planetModel.ImagePath
            };
        }
    }
}