using LionStore.Api.Configuration;
using LionStore.Api.OpenApi;
using LionStore.Core;
using LionStore.Core.Models.Identity;
using LionStore.Core.Repositories;
using LionStore.Core.Services;
using LionStore.Data;
using LionStore.Data.Repositories;
using LionStore.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace LionStore.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration);
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>();
            services.AddScoped(s => s.GetRequiredService<IOptionsSnapshot<AppSettings>>().Value);

            services.AddAuthentication();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                Configuration.Bind($"{nameof(AppSettings.Security)}:{nameof(AppSettings.Security.Jwt)}", options);
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false
                };
            })
            .Services
            .AddHttpClient()
            .AddSwaggerGen();

            // Register the DbContext
            var dataAssemblyName = typeof(LionStoreDbContext).Assembly.GetName().Name;
            services.AddDbContext<LionStoreDbContext>(options =>
#if DEBUG
            options.UseMySql("server=127.0.0.1;database=lion_store;user=lion;password=lion;Allow Zero Datetime=true;Convert Zero Datetime=true",
#else
            options.UseMySql(Configuration.GetConnectionString("LionStoreContext"),
#endif
            mySqlOptionsAction: sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(dataAssemblyName);
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(2),
                    errorNumbersToAdd: null);
            }));

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IOrderService, OrderService>();

            services.AddAutoMapper(typeof(Startup));

            services.AddHttpContextAccessor();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var appSettings = Configuration.Get<AppSettings>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.RoutePrefix = "";
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "LionStore API V1");
                options.OAuthClientId(appSettings.Security.Jwt.ClientId);
                options.OAuthClientSecret(appSettings.Security.Jwt.ClientSecret);
                options.OAuthAppName("LionStore API");
                //options.OAuthScopeSeparator(" ");
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