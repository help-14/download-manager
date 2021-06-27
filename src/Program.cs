using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Runtime.InteropServices;

if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
    Host.CreateDefaultBuilder(args).ConfigureServices((hostContext, services) => { services.AddHostedService<Service>(); }).Build().Run();
else
    new WebServer().Run();