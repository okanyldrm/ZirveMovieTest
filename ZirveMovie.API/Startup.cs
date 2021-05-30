using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Zirve.Business.Abstract;
using Zirve.Business.Concrete;
using Zirve.DataAccess.Abstract;
using Zirve.DataAccess.Concrete;
using ZirveMovie.API.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Zirve.DataAccess;
using Zirve.DataAccess.Concrete.Context;
using Zirve.Entity.Concrete;
using ZirveMovie.API.Controllers;

namespace ZirveMovie.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionServices.Set(configuration);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ZirveMovie.API", Version = "v1" });
            });

            ////Entity Framework
            services.AddDbContext<ZirveMovieContext>(option =>
                option.UseSqlServer(Configuration.GetConnectionString("ConStr")));


            //For Identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ZirveMovieContext>()
                .AddDefaultTokenProviders();

            //Adding Authentication
            services.AddAuthentication(option =>
                {
                    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                }) 
                //Adding Jwt Bearer
                .AddJwtBearer(option =>
                {
                    option.SaveToken = true;
                    option.RequireHttpsMetadata = false;
                    option.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidAudience = Configuration["JWT:ValidAudience"],
                        ValidIssuer = Configuration["JWT:ValidIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                    };
                });
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

            services.AddAutoMapper(typeof(Startup));

            ////hangfire
            services.AddHangfire(x => x.UseSqlServerStorage("Server=DESKTOP-J0M3R33; Database=zirvemoviedbhangfire; Trusted_Connection=True"));
            services.AddHangfireServer();


            //DI
            services.AddScoped<ITheMovieDbService, TheMovieDbManager>();
            services.AddScoped<ITheMovieDbDal, TheMovieDbDal>();

            services.AddScoped<IMovieDal, MovieDal>();
            services.AddScoped<IMovieService, MovieManager>();

            services.AddScoped<IEvaluationDal, EvaluationDal>();
            services.AddScoped<IEvaluationService, EvaluationManager>();

            services.AddScoped<INoteDal, NoteDal>();
            services.AddScoped<INoteService, NoteManager>();

            services.AddScoped<IEmailDal, EmailDal>();
            services.AddScoped<IEmailService, EmailManager>();


        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ZirveMovie.API v1"));
            }

            //Hangfire
            app.UseHangfireDashboard();
            TheMovieDbServiceController h = new TheMovieDbServiceController();
            RecurringJob.AddOrUpdate(() => h.GetMovie(), Cron.Minutely());


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
