using API.Data;
using API.Mapper;
using API.Repository;
using API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(_config.GetConnectionString("DefaultConnection"));

            });

            services.AddControllers();
            services.AddScoped<IUserGroupRepository, UserGroupRepository>();
            services.AddAutoMapper(profileAssemblyMarkerTypes: typeof(Mappings));
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("XenergyAPISpec", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "XenergyAPI",
                    Version = "v1",
                    Description = "Xenergy User API",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                    {
                        Email = "srt.karunarathna@gmail.com",
                        Name = "Sanjaya"
                    },
                    License = new Microsoft.OpenApi.Models.OpenApiLicense()
                    {
                        Name = "MIT License"
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/XenergyAPISpec/swagger.json", "Xenergy API");
            });
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
