using System;
using System.Collections.Generic;
using Galaxy.Planets.Core.Enums;

namespace Galaxy.Planets.Core.Models
{
    public class Team
    {
        public Guid Id { get; set; }
        public TeamStatus Status { get; set; }
        public List<Guid> Robots {get; set;}
    }
}