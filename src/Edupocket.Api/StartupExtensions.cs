using Edupocket.Application;
using Edupocket.DAL;
using System.Reflection;

namespace Edupocket.Api
{
    /// <summary>
    /// StartExtensions
    /// </summary>
    public static class StartupExtensions
    {
        /// <summary>
        /// Configure the services
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(p =>
            {
                p.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Edupocket Wallet System", Version = "v1" });
                var xmlFileName = $"{Assembly.GetEntryAssembly()?.GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFileName);

                p.IncludeXmlComments(xmlPath);

            });

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
