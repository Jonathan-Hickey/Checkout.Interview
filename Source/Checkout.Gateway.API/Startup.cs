using System.Text;
using Checkout.Gateway.API.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Checkout.Gateway.API
{
    public class Startup
    {
        private static readonly string _internalServerErrorMessage = "A team of highly trained monkeys has been dispatched";
        private static readonly byte[] _internalServerErrorMessageBytes = Encoding.UTF8.GetBytes(_internalServerErrorMessage);

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddAuthentication("Bearer")
                    .AddOAuth2Introspection(options =>
                    {
                        options.Authority = "https://localhost:5002";
                        options.ClientId = "PaymentGateway";
                        options.ClientSecret = "secret";
                    }
                );

            services.RegisterValidators();
            services.RegisterMappers();
            services.RegisterServices();
            services.RegisterClients();
            services.RegisterRepositories();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "text/html";

                    await context.Response.Body.WriteAsync(_internalServerErrorMessageBytes, 0, _internalServerErrorMessageBytes.Length);
                });
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
