using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.WindowsServices;
using System.Diagnostics;
using System.Web.Services.Description;
namespace TestCoreWCFServer
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class MaintenanceService : IMaintenanceService
    {
        public IRandomCodeGenerator RandomCodeGenerator { get;  }
        public MaintenanceService(IRandomCodeGenerator randomCodeGenerator) {

            RandomCodeGenerator = randomCodeGenerator;
        }
        public string ReceivedByServer(string message)
        {
            Console.WriteLine($"Random number for {message}: " + RandomCodeGenerator.GenerateRandomCode());

            return RandomCodeGenerator.GenerateRandomCode();
        }
        public WebApplication CreateHost()
        {
            //   Debugger.Break();
            var options = new WebApplicationOptions
            {
                ContentRootPath = Directory.GetCurrentDirectory()//WindowsServiceHelpers.IsWindowsService() ? default : System.AppContext.BaseDirectory  //
            };
            var builder = WebApplication.CreateBuilder();

            builder.Services.AddServiceModelServices();
            builder.Services.AddServiceModelMetadata();
            //builder.WebHost.ConfigureKestrel(serverOptions =>
            //{
            //    serverOptions.ListenAnyIP(8000); 
            //});

            builder.Services.AddSingleton<IRandomCodeGenerator>(sp =>
            {
                return new RandomNumberGenerator();
            });
            builder.Services.AddSingleton<MaintenanceService>();
            
            builder.WebHost.UseNetNamedPipe(options =>
            {
                options.Listen(new Uri("net.pipe://localhost/maintenance/"));
            });
            var app = builder.Build();

            app.UseServiceModel(serviceBuilder =>
            {
                serviceBuilder.AddService<MaintenanceService>();
                serviceBuilder.AddServiceEndpoint<MaintenanceService>(typeof(IMaintenanceService), new NetNamedPipeBinding(NetNamedPipeSecurityMode.None) { ReceiveTimeout = TimeSpan.MaxValue, CloseTimeout = TimeSpan.MaxValue, SendTimeout = TimeSpan.MaxValue }, "net.pipe://localhost/maintenance/");

                serviceBuilder.ConfigureServiceHostBase<IMaintenanceService>(serviceHost =>
                {
                    var behavior = new ServiceBehaviorAttribute();
                    behavior.IncludeExceptionDetailInFaults = true;
                    behavior.InstanceContextMode = InstanceContextMode.Single;
                    behavior.ConcurrencyMode = ConcurrencyMode.Multiple;
                    serviceHost.Description.Behaviors.Add(behavior);

                });
                var serviceMetadataBehavior = app.Services.GetRequiredService<ServiceMetadataBehavior>();
                serviceMetadataBehavior.HttpsGetEnabled = true;
            });
            return app;
        }
    }
}
