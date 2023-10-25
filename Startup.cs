using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using WebApiMongoDB.Controllers.Dtos;
using WebApiMongoDB.Repository.MongoDB_g;
using WebApiMongoDB.Service.MongoDB;

namespace WebApiMongoDB
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
            
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IMongoDBService<PersonDto>, PersonService>();
            services.AddScoped<IMongoDBService<ProductDto>, ProductService>();
           
          

            //mongoDB genérico
            services.Configure<MongoDbSettings>(Configuration.GetSection("DevNetStoreDatabase"));

            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));


            services.AddSingleton<IMongoDbSettings>(serviceProvider =>
                serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);

            //    services.AddScoped(typeof(IRepositoryMongo<>), typeof(RepositoryMongo<>));

            services.AddControllers(o =>
            {
               
            });

            // cache in memory
            services.AddMemoryCache();
            services.AddSwaggerGen();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //exception handler with Azure Service Bus
            //  app.ConfigureExceptionHandler(new AzureServiceBus(Configuration));

            app.UseRouting();

            //enables swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Brf.ProducerSAP DevOps v1");
            });


            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
