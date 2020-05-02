namespace WebAppSome
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.IdentityModel.Tokens;
    using WebAppSome.DataAccess;
    using WebAppSome.DataAccess.Entities;
    using WebAppSome.DataAccess.Repositories;
    using WebAppSome.Infrastructure;

    public class Startup
    {
        public Startup(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            Configuration = configuration;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .AddConfiguration(configuration);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options => 
                options.UseSqlServer(this.Configuration["Data:ConectionString"], o => o.MigrationsAssembly("WebAppSome")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<DataContext>();

            //Auth
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                //.AddJwtBearer(options => // token generating by app
                //{
                //    options.RequireHttpsMetadata = false;
                //    options.TokenValidationParameters = new TokenValidationParameters
                //    {
                //        // укзывает, будет ли валидироваться издатель при валидации токена
                //        ValidateIssuer = true,
                //        // строка, представляющая издателя
                //        ValidIssuer = AuthOptions.ISSUER,

                //        // будет ли валидироваться потребитель токена
                //        ValidateAudience = true,
                //        // установка потребителя токена
                //        ValidAudience = AuthOptions.AUDIENCE,

                //        // будет ли валидироваться время существования
                //        ValidateLifetime = true,

                //        // валидация ключа безопасност
                //        ValidateIssuerSigningKey = true,
                //        // установка ключа безопасности
                //        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey()
                //    };
                //});
                //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                //    .AddCookie(options => // CookieAuthenticationOptions
                //    {
                //        options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                //    });


            // Resolve dependencies
            services.AddScoped<IRepository, Repository>();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Use with default auth and with tokens
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
