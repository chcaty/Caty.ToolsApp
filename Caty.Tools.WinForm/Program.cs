using Caty.Tools.Model;
using Caty.Tools.Model.Rss;
using Caty.Tools.Share;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Caty.Tools.WinForm;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        //Application.Run(new FrmMain());
        var service = new ServiceCollection();
        ConfigureServices(service);

        var serviceProvider = service.BuildServiceProvider();
        var frmMain = serviceProvider.GetRequiredService<FrmMain>();
        Application.Run(frmMain);
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<FrmMain>();

        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("config.json", false, true)
            .AddJsonFile("appsettings.json", false, true);

        IConfiguration configuration = builder.Build();
        services.AddSingleton(configuration);

        services.AddOptions();
        services.AddShareServiceModule();
        services.AddApplicationEntitiesModule(configuration);
        services.Configure<List<RssSource>>(configuration.GetSection("RssSources"));
    }
}