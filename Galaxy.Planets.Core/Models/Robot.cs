using System;
using Galaxy.Planets.Core.Enums;

namespace Galaxy.Planets.Core.Models
{
    public class Robot
    {
        public Guid Id { get; set; }
        public RobotStatus Status { get; set; }
    }
}