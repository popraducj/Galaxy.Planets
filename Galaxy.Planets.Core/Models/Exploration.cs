using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Galaxy.Planets.Core.Enums;

namespace Galaxy.Planets.Core.Models
{
    [Table("Explorations")]
    public class Exploration : Entity
    {
        [Required]
        [Column(TypeName = "varchar(256)")]
        public string Name { get; set; }
        public Guid PlanetId { get; set; }
        public Guid TeamId { get; set; }
        public ExplorationStatus Status { get; set; }
        public DateTime? PhaseFinishTime { get; set; }
        
        public List<ExplorationResultStatus> RobotsReports { get; set; }
    }
}