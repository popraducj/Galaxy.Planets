using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Galaxy.Planets.Core.Enums;

namespace Galaxy.Planets.Core.Models
{
    [Table("Planets")]
    public class Planet : Entity
    {
        [Required]
        [Column(TypeName = "varchar(256)")]
        public string Name { get; set; }
        [Column(TypeName = "varchar(2048)")]
        public string ImagePath { get; set; }
        [Column(TypeName = "varchar(2500)")]
        public string Description { get; set; }
        public int Units { get; set; }
        public PlanetStatus Status { get; set; }
    }
}