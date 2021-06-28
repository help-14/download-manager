using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

public class WebServer : IDisposable
{
    WebApplication app;

    public WebServer()
    {
        var builder = WebApplication.CreateBuilder();
        builder.Services.AddControllers();

        app = builder.Build();
        app.MapControllers();

        if (app.Environment.IsDevelopment())
            app.UseDeveloperExceptionPage();

        Config.Load();
    }

    public Task RunAsync()
    {
        return app.RunAsync();
    }

    public void Run()
    {
        app.Run();
    }

    public void Dispose()
    {
        app?.StopAsync();
        app?.DisposeAsync();
        app = null;
    }

}