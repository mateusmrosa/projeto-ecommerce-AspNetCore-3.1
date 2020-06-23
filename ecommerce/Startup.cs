using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace ecommerce
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
            #region Obtendo as configurações do projeto
            AppSettings appSettings = new AppSettings();
            //responsavel pela deserializanção do appsettings.json
            var config = new ConfigureFromConfigurationOptions<object>
                (Configuration.GetSection("AppSettings"));
            config.Action(appSettings);

            services.AddSingleton<AppSettings>(appSettings);

            //variavel global para string de conexao
            System.Environment.SetEnvironmentVariable("MYSQLSTRCON", appSettings.StringConexaoMysql);

            #endregion

            #region Serviço para Cookie Authorization

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            }).AddCookie(option =>
            {
                option.Cookie.Name = "CookieAutenticacao";
                option.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Default/Index");
                option.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Default/Index");
                option.ExpireTimeSpan = TimeSpan.FromDays(appSettings.CookieTempoVida);
            });

            services.AddAuthorization(option =>
            {
                option.AddPolicy("CookieAutenticacao", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });

            #endregion

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddControllersWithViews();

            services.AddHttpContextAccessor();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Default}/{action=Index}/{id?}");
            });
        }
    }
}
