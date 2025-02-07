using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCoreWCFServer
{
    public class ServiceHostBuilder
    {    
            public void ServiceHost()
            {
                IHost host = Host.CreateDefaultBuilder()

                      .UseWindowsService(options =>
                      {
                          options.ServiceName = "TestServer";

                      })
                      .UseContentRoot(Directory.GetCurrentDirectory())
                      .ConfigureServices(services =>
                      {
                          services.AddHostedService<WorkerService>();
                          services.AddWindowsService();
                          //services.AddSingleton<MaintenanceService>();
                      })
                      .Build();


                host.Run();

            }
        
    }
}
