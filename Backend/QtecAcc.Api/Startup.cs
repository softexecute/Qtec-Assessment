using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QtecAcc.Application.Validations;
using QtecAcc.Infrastructure;
using System.Reflection;
namespace QtecAcc
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {

           

            services.AddControllers()
                            .AddFluentValidation(fv => 
                            fv.RegisterValidatorsFromAssemblyContaining<CreateAccountDtoValidator>()
                            
                            );

            services.AddEndpointsApiExplorer();
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                             .EnableDetailedErrors();
            });
            services.AddScoped<IDbContext, ApplicationDbContext>();
            services.AddSwaggerGen();

            services.AddScoped<IDbContext, ApplicationDbContext>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg=>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            // Optional CORS policy (example)
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("AllowAll"); // just for test
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
