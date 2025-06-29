using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using QtecAcc.Api.Handler;
using QtecAcc.Application;
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

            services.AddControllers();

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<CreateAccountCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateJournalCommandValidator>();
    


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

            services.MediatorDependency();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            services.AddExceptionHandler<CustomExceptionHandler>();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandler(_ => { });

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
