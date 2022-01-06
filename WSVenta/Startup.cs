using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using WSVenta.Models.Common;
using WSVenta.Services;
using Microsoft.IdentityModel.Tokens;

namespace WSVenta
{
    public class Startup
    {
        //configuración del CORS
        readonly string MiCors = "MiCors";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(opciones =>
            {
                opciones.AddPolicy(name: MiCors,
                                    builder => 
                                    {
                                        builder.WithHeaders("*");//permite headers para los métodos post
                                        builder.WithOrigins("*");//agregar las url o * para permitir todo
                                        builder.WithMethods("*");//permite métodos
                                    });
            });
            services.AddControllers();

            //configuración para JWT
            var appSettingsSection = Configuration.GetSection("AppSettings");//obtenemos la sección del appsettings.json
            services.Configure<AppSettings>(appSettingsSection); //inyectamos a la clase creada
            //JWT
            var appSettings = appSettingsSection.Get<AppSettings>();//obtener lo que hicimos en los renglones anteriores
            var llave = Encoding.ASCII.GetBytes(appSettings.Secreto);//bytes[] de secreto
            //damos de alta la autentificación
            services.AddAuthentication(d =>
            {
                d.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                d.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(d=>
                {
                    d.RequireHttpsMetadata = false;
                    d.SaveToken = true;
                    d.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(llave),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IVentaService, VentaService>();
            //services.AddScoped<IVentaService, VentaService2>(); // CUANDO SON VARIAS CLASES QUE IMPLEMENTAN UNA INTERFAZ
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MiCors);
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
