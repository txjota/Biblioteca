using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Biblioteca.Services;
using Biblioteca.Models;

namespace Biblioteca
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
            services.AddControllersWithViews();

            services.AddScoped<BibliotecaContext>();

            // Registra UsuarioService
            services.AddScoped<UsuarioService>();
            services.AddScoped<AuthService>();

            // Configuração do serviço de sessão
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Tempo de expiração da sessão
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            // Configuração do serviço de autenticação baseada em cookies
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Home/Login"; // Defina aqui o caminho para a sua ação de login
                    options.AccessDeniedPath = "/Home/AccessDenied"; // Defina aqui o caminho para a ação de acesso negado
                });

            // Configuração do Entity Framework Core
            //services.AddDbContext<BibliotecaContext>(options =>
                //options.UseSqlServer(Configuration.GetConnectionString("BibliotecaConnection")));

            // Registro do serviço de usuário
            services.AddScoped<UsuarioService>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Configuração de autenticação e autorização
            app.UseAuthentication();
            app.UseAuthorization();

            // Configuração de sessão
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
