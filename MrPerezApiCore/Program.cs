using MrPerezApiCore.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AspNetCoreRateLimit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddJsonFile("appsettings.json");
var secretKey = builder.Configuration.GetSection("settings").GetSection("secretkey").ToString();
var keyByte = Encoding.UTF8.GetBytes(secretKey);

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyByte),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<EmpresaData>();
builder.Services.AddSingleton<DepartamentoData>();
builder.Services.AddSingleton<AutenticacionData>();
builder.Services.AddSingleton<UsuarioData>();
builder.Services.AddSingleton<RolData>();
builder.Services.AddSingleton<PaginaAccesoData>();
builder.Services.AddSingleton<MarcasData>();
builder.Services.AddSingleton<CategoriasData>();
builder.Services.AddSingleton<GeneroData>();
builder.Services.AddSingleton<MetodosPagoData>();
builder.Services.AddSingleton<EmpleadoData>();
builder.Services.AddSingleton<ProductosData>();
builder.Services.AddSingleton<CarritoData>();
builder.Services.AddSingleton<OrdenesData>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", app =>
    {
        app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.Configure<IpRateLimitPolicies>(builder.Configuration.GetSection("IpRateLimitPolicies"));
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

var app = builder.Build();

var env = app.Services.GetRequiredService<IWebHostEnvironment>();
if (env.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
        options.DocumentTitle = "API Mr Perez Pijamas";
    });
}

app.UseHttpsRedirection();

app.UseRouting(); // Agrega el middleware de enrutamiento de puntos finales aquí

app.UseCors("NuevaPolitica");
app.UseAuthentication();
app.UseAuthorization();

app.Use((context, next) =>
{
    context.Request.EnableBuffering();
    return next();
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();