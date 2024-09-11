using Edupocket.Application;
using Edupocket.DAL;

namespace Edupocket.Api
{
    public static class StartupExtensions
    {
        // Add services to the container.
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddApplicationServices();
            builder.Services.AddDataAccessServices(builder.Configuration);
            builder.Services.AddOtherService(builder.Configuration);

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddControllers();
            builder.Services.AddCors(o =>
            {
                o.AddPolicy("EdupocketWallet", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            builder.Services.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                o.ReportApiVersions = true;
            });

            builder.Services.AddHealthChecks();

            return builder.Build();
        }

        // Configure the HTTP request pipeline.
        public static WebApplication ConfigurePipeline(this WebApplication app) 
        {
            if (app.Environment.IsDevelopment()) 
            {
                app.UseSwagger();
                app.UseSwaggerUI(x =>
                {
                    x.SwaggerEndpoint("/swagger/v1/swagger.json", "Edupocket Wallet API");
                });
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseHealthChecks("/health");
            app.UseHealthChecks("/health/ping", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions()
            {
                Predicate = (check) => check.Tags.Contains("ping")
            });

            app.UseCors("EdupocketWallet");
            app.MapControllers();

            return app;
        }

    }
}
