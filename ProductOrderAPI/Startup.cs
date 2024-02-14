using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using RepositoryLibrary;
using Microsoft.EntityFrameworkCore;
using UtilitiesLibrary;
using ServicesLibrary;
using InterfaceLibrary;
using Microsoft.OpenApi.Models;


namespace ProductOrderAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<ProductOrderDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //Register InputParameterValidation as singleton
            //services.AddSingleton<InputParameterValidator>();

            //Use existing instances for InputParameterValidator
            services.AddScoped(provider => InputParameterValidator.inputParameterValidator);
            //Register Services
            services.AddScoped<IProductRepository,ProductRepository>();
            services.AddScoped<IOrderRepository,OrderRepository>();
            //builder.Services.AddScoped<InputParameterValidator>();
            services.AddScoped<MySqlException>();
            services.AddScoped<ProductService>();
            services.AddScoped<OrderService>();
            //Add Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProductOrderAPI", Version = "v1" });
            });
            //Policy Based Autherization

            //services.AddAuthorization(options =>
            // {
            //     options.AddPolicy("AdminRole", policy => policy.RequireRole("Admin"));
            // });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                //app.UseSwaggerUI();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductOrderAPI");
            });
           
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
        
    }
}
