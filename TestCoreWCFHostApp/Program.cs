using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestCoreWCFServer;

namespace TestCoreWCFHostApp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Calling CreateHost");

            ServiceHostBuilder builder = new ServiceHostBuilder();
            builder.ServiceHost();
        }
    }

    //internal class ServiceHostBuilder
    //{
    //    public void ServiceHost()
    //    {
    //        IHost host = Host.CreateDefaultBuilder()
                 
    //              .UseWindowsService(options =>
    //              {
    //                  options.ServiceName = "TestServer";
                      
    //              })
                  
    //              .ConfigureServices(services =>
    //              {
    //                  services.AddHostedService<WorkerService>();
    //                  services.AddWindowsService();
    //                  //services.AddSingleton<MaintenanceService>();
    //              })
    //              .Build();
            

    //        host.Run();

    //    }
    //}
}
