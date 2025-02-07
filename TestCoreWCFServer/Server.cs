using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace TestCoreWCFServer
{
    public class Server
    {
        public MaintenanceService MaintenanceService { get; }
        public Server() {

            var random = new RandomNumberGenerator();

            MaintenanceService = new MaintenanceService(random); 
        
        }
      
    }
}
