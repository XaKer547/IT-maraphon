using FluentValidation;
using Hangfire;
using IT_maraphon.API.Helpers;
using IT_maraphon.API.Services;
using IT_maraphon.API.Validators.Behaviors;
using IT_maraphon.Application.Services;
using IT_maraphon.DataAccess.Data.Entities;
using IT_maraphon.Domain.Services;
using Microsoft.Extensions.FileProviders;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

namespace IT_maraphon.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.ConfigureStaticFilesFolder();

        builder.Services.AddControllers()
            .AddNewtonsoftJson();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen(c =>
        {
            c.ExampleFilters();
        });

        builder.Services.AddSingleton<ICanvasProviderConfiguration, CanvasProviderConfiguration>();

        builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();

        builder.Services.AddScoped<ICanvasService, CanvasService>();

        builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

        builder.Services.AddAutoMapper(config =>
        {
            config.CreateMap<Dictionary<string, object>, Circle>();
            config.CreateMap<Dictionary<string, object>, Rectangle>();
        });

        builder.Services.AddMediatR(m =>
        {
            m.RegisterServicesFromAssembly(Assembly.Load("IT-maraphon.Application"));

            m.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        builder.Services.AddHangfire(configuration => configuration
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseInMemoryStorage());

        builder.Services.AddScoped((provider) => JobStorage.Current.GetConnection());

        builder.Services.AddHangfireServer();

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        app.UseHangfireDashboard();

        app.MapControllers();

        app.MapHangfireDashboard();

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(builder.Environment.ContentRootPath),
            RequestPath = "/schemes"
        });

        app.Run();
    }
}