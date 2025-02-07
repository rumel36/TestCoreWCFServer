using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCoreWCFServer
{
    public class WorkerService : BackgroundService, IHostedLifecycleService //BackgroundService//IHostedService
    {
        private MaintenanceService MaintenanceService { get; set; }
        private WebApplication _host;
        public WorkerService() {

            Server server = new Server();
            MaintenanceService = server.MaintenanceService;
        }

        public Task StartedAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task StartingAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task StoppedAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;  
        }

        public Task StoppingAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _host = MaintenanceService.CreateHost();
            _host.Run();
            return Task.CompletedTask;
         
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (stoppingToken.IsCancellationRequested) {

                Console.WriteLine("Service Running");
                await Task.Delay(1000);
            }
        }
        //public Task StartAsync(CancellationToken cancellationToken)
        //{
        //    //Server server = new Server();
        //    //var maintenance = server.MaintenanceService;
        //    _host = MaintenanceService.CreateHost();
        //    _host.Run();
        //    return Task.CompletedTask;
        //}

        //public Task StopAsync(CancellationToken cancellationToken)
        //{
        //    //throw new NotImplementedException();
        //     _host.StopAsync();
        //    return Task.CompletedTask;
        //}

        //protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        //{
        //    Server server = new Server();
        //    var maintenance = server.MaintenanceService;
        //    while (true)
        //    {
        //        maintenance.CreateHost();
        //       //await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        //    }
        //}
    }
}
