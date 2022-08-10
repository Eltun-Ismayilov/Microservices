using APIGateway;

public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHost(args).Build();

        host.Run();
    }

    private static IHostBuilder CreateHost(string[] args)
    {
        var builder = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, configurationBuilder) =>
            {
                configurationBuilder.AddJsonFile("ocelot.json");
            })
            .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());

        return builder;
    }

}