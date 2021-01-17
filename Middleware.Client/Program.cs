using Microsoft.Extensions.DependencyInjection;
using Middleware.Client.Interfaces;
using System;
using System.Threading.Tasks;

namespace Middleware.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //Console.WriteLine("Test client");

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            try
            {
                await serviceProvider.GetService<IMiddlewareService>().Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddHttpClient();
            //serviceCollection.AddHttpClient<MiddlewareClientService>();

            serviceCollection.AddScoped<IMiddlewareService, MiddlewareClientService>();
        }
    }
}
