using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using BookBook.Repositories;
using BookBook.Manager;
using Microsoft.Extensions.Azure;
using Azure.Storage.Queues;
using Azure.Storage.Blobs;
using Azure.Core.Extensions;
using System;
using BookBook.Data;

namespace BookBook
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
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );

            services.AddSingleton(x => new BlobServiceClient(Configuration.GetConnectionString("AzureStorageConnection")));
            services.AddScoped<IBlobService, BlobService>();
            services.AddAzureClients(builder =>
            {
                builder.AddBlobServiceClient(Configuration["AzureStorageConnection:blob"], preferMsi: true);
                builder.AddQueueServiceClient(Configuration["AzureStorageConnection:queue"], preferMsi: true);
            });

            services.AddSingleton<IUserActivitiesManager, UserActivitiesManager>();

            services.AddScoped<IResetPasswordManager, ResetPasswordManager>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IResetPasswordRepository, ResetPasswordRepository>();

            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IMovieStaffRepository, MovieStaffRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<ITheaterRepository, TheaterRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ITheaterProductRepository, TheaterProductRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookBook", Version = "v1" });
                c.OperationFilter<SwaggerFileOperationFilter>();
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => {
                    c.SwaggerEndpoint("v1/swagger.json", "BookBook v1");
                });

            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
    internal static class StartupExtensions
    {
        public static IAzureClientBuilder<BlobServiceClient, BlobClientOptions> AddBlobServiceClient(this AzureClientFactoryBuilder builder, string serviceUriOrConnectionString, bool preferMsi)
        {
            if (preferMsi && Uri.TryCreate(serviceUriOrConnectionString, UriKind.Absolute, out Uri serviceUri))
            {
                return builder.AddBlobServiceClient(serviceUri);
            }
            else
            {
                return builder.AddBlobServiceClient(serviceUriOrConnectionString);
            }
        }
        public static IAzureClientBuilder<QueueServiceClient, QueueClientOptions> AddQueueServiceClient(this AzureClientFactoryBuilder builder, string serviceUriOrConnectionString, bool preferMsi)
        {
            if (preferMsi && Uri.TryCreate(serviceUriOrConnectionString, UriKind.Absolute, out Uri serviceUri))
            {
                return builder.AddQueueServiceClient(serviceUri);
            }
            else
            {
                return builder.AddQueueServiceClient(serviceUriOrConnectionString);
            }
        }
    }
}
