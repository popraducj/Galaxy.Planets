using System;
using System.Collections.Generic;
using Galaxy.Planets.Core.Enums;
using Galaxy.Planets.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace Galaxy.Planets.Infrastructure
{
    public class PlanetsDbContext : DbContext
    {
        public PlanetsDbContext(DbContextOptions options)
            : base(options)
        {
        }
        
        public DbSet<Planet> Planets { get; set; }
        public DbSet<Exploration> Explorations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            AddConvertedProperties(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }


        private static void AddConvertedProperties(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exploration>().Property(x => x.RobotsReports).HasConversion(
                new ValueConverter<List<ExplorationResultStatus>, string>(
                    v => v == null? null: JsonConvert.SerializeObject(v),
                    v => v== null ? null: JsonConvert.DeserializeObject<List<ExplorationResultStatus>>(v)));
        }
    }
}