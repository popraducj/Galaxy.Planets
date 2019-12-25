using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Galaxy.Planets.Core.Enums;
using Galaxy.Planets.Core.Interfaces;
using Galaxy.Planets.Core.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Galaxy.Planets.Core
{
    public class ExplorationCheck : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceFactory;
        private bool _hasFinished = true;

        public ExplorationCheck(IServiceScopeFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }
        
        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await ExplorationUpdateStatus();
            var timer = new System.Timers.Timer(1000);
            timer.Elapsed += async (sender, args) => { await ExplorationUpdateStatus(); };
            timer.Start();
        }

        private async Task ExplorationUpdateStatus()
        {
            if (!_hasFinished) return;
            _hasFinished = false;
            using (var scope = _serviceFactory.CreateScope())
            {
                var exploringService = scope.ServiceProvider.GetService<IService<Exploration>>();
                var all = await exploringService.GetAllAsync();
                var date = DateTime.UtcNow;

                var explorations = all.Where(x => x.PhaseFinishTime.HasValue && x.PhaseFinishTime.Value < date).ToList();
                foreach (var exploration in explorations)
                {
                    exploration.Status = exploration.Status == ExplorationStatus.EnRouteToPlanet ?
                                        ExplorationStatus.Exploring : ExplorationStatus.Finished;
                    await exploringService.UpdateAsync(exploration);
                } 
            }

            _hasFinished = true;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
            }
        }
    }
}