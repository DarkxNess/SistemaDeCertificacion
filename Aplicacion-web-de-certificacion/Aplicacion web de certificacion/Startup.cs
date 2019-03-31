using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Aplicacion_web_de_certificacion.Data;
using Aplicacion_web_de_certificacion.Models;
using Aplicacion_web_de_certificacion.Services;
using Microsoft.Extensions.Logging;

namespace Aplicacion_web_de_certificacion
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
            options.UseMySQL(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Añadir aplicacion como servicio, bases de datos, etc.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddMvc();
            services.Add(new ServiceDescriptor(typeof(EntidadesContext), new EntidadesContext(Configuration.GetConnectionString("DefaultConnection"))));
            services.Add(new ServiceDescriptor(typeof(UsuariosContext), new UsuariosContext(Configuration.GetConnectionString("DefaultConnection"))));
            services.Add(new ServiceDescriptor(typeof(ContactoContext), new ContactoContext(Configuration.GetConnectionString("DefaultConnection"))));
            services.Add(new ServiceDescriptor(typeof(LugarEnsayosContext), new LugarEnsayosContext(Configuration.GetConnectionString("DefaultConnection"))));
            services.Add(new ServiceDescriptor(typeof(PresupuestoContext), new PresupuestoContext(Configuration.GetConnectionString("DefaultConnection"))));
            services.Add(new ServiceDescriptor(typeof(ProductosContext), new ProductosContext(Configuration.GetConnectionString("DefaultConnection"))));
            services.Add(new ServiceDescriptor(typeof(CotizacionesContext), new CotizacionesContext(Configuration.GetConnectionString("DefaultConnection"))));
            services.Add(new ServiceDescriptor(typeof(ListaPreciosContext), new ListaPreciosContext(Configuration.GetConnectionString("DefaultConnection"))));
            services.Add(new ServiceDescriptor(typeof(PedidoEnsayoContext), new PedidoEnsayoContext(Configuration.GetConnectionString("DefaultConnection"))));
            services.Add(new ServiceDescriptor(typeof(MuestrasContext), new MuestrasContext(Configuration.GetConnectionString("DefaultConnection"))));
            services.Add(new ServiceDescriptor(typeof(AprobacionContext), new AprobacionContext(Configuration.GetConnectionString("DefaultConnection"))));
            services.Add(new ServiceDescriptor(typeof(EnsayosContext), new EnsayosContext(Configuration.GetConnectionString("DefaultConnection"))));
            services.Add(new ServiceDescriptor(typeof(PerfilesContext), new PerfilesContext(Configuration.GetConnectionString("DefaultConnection"))));
            services.Add(new ServiceDescriptor(typeof(ArchivosContext), new ArchivosContext(Configuration.GetConnectionString("DefaultConnection"))));


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
       

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            //app.UseCookieAuthentication(options =>
            //{
            //    options.AuthenticationScheme = "Cookies";
            //    options.LoginPath = new PathString("/Account/Login");
            //    options.AutomaticAuthenticate = true;
            //    options.AutomaticChallenge = true;
            //});


            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            CreateRoles(serviceProvider).Wait();
        }
        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var roleMagar = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userMagar = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] rolesNames = { "Admin", "JefeLab", "JefeOCP", "Tecnico", "OCP", "Comercial" };
            IdentityResult result;
            foreach (var rolesName in rolesNames)
            {
                var roleExist = await roleMagar.RoleExistsAsync(rolesName);
                if (!roleExist)
                {
                    result = await roleMagar.CreateAsync(new IdentityRole(rolesName));
                }
            }

        }
    }
}

