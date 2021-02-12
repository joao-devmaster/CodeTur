using CodeTur.Dominio.Handlers.Pacotes;
using CodeTur.Dominio.Handlers.Usuarios;
using CodeTur.Dominio.Repositorios;
using CodeTur.Infra.Data.Contexts;
using CodeTur.Infra.Data.Repositorios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace CodeTur.Api
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
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                //Correção do erro object cycle
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                //Remover propriedades nulas
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });

            services.AddDbContext<CodeTurContext>(o => o.UseSqlServer
                (@"Data Source=DESKTOP-8NPEQKL\SQLEXPRESS;
                Initial Catalog=CodeTur;
                user id=sa;
                password=sa132"));

            // JWT
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "codetur",
                        ValidAudience = "codetur",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SecretKeyCODETUR"))
                    };
                });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api CodeTur", Version = "v1" });
            });

            #region Injeção Dependência Usuario
            //registrando IUsuarioRepositorio como service
            services.AddTransient<IUsuarioRepositorio, UsuarioRepositorio>();
            
            //registrando CriarUsuarioHandle como service
            services.AddTransient<CriarUsuarioCommandHandle, CriarUsuarioCommandHandle>();
            
            //registrando LogarUsuarioHandle como service
            services.AddTransient<LogarUsuarioCommandHandle, LogarUsuarioCommandHandle>();
            #endregion

            #region Injeção Dependência Pacote
            //registrando IPacoteRepositorio como service
            services.AddTransient<IPacoteRepositorio, PacoteRepositorio>();

            //registrando CriarPacoteHandle como service
            services.AddTransient<CriarPacoteCommandHandle, CriarPacoteCommandHandle>();
            
            //registrando ListarPacotesHandle como service
            services.AddTransient<ListarPacotesQueryHandle, ListarPacotesQueryHandle>();

            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API CodeTur V1"));

            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(c => c.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
