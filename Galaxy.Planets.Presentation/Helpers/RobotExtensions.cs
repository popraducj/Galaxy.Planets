using System;
using Galaxy.Planets.Core.Enums;
using Galaxy.Teams;
using Robot = Galaxy.Planets.Core.Models.Robot;

namespace Galaxy.Planets.Presentation.Helpers
{
    public static class RobotExtensions
    {
        public static RobotModel ToRobotModel(this Robot robot)
        {
            if(robot == null) return new RobotModel();
            return new RobotModel
            {
                Id = robot.Id.ToString(),
                Name =  string.Empty,
                Status = (int) robot.Status,
                Manufacturer = string.Empty,
                Model = string.Empty,
                NextRevision = string.Empty
            };
        }
        public static Robot ToRobot(this RobotModel robot)
        {
            return new Robot
            {
                Id = Guid.Parse(robot.Id),
                Status = (RobotStatus) robot.Status
            };
        }
    }
}