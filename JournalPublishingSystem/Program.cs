using Infrastructure.Constants;
using Infrastructure.Contract;
using Infrastructure.DbConnection;
using Infrastructure.Contract;
using Infrastructure.DbConnection;
using DataAccess.Repository;
using DataAccess.Contracts;
using Application.Contracts;
using Application.Services;
using Application.Profiles;
using AutoMapper;
using Microsoft.Extensions.Options;
using Infrastructure.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Infrastructure.Services;
using Infrastructure.Services.Infrastructure.Security;
using DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using Serilog.Sinks.MSSqlServer;
using Serilog;
using Infrastructure.ExceptionHandling;
using  FluentValidation.AspNetCore;
using FluentValidation;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<ConnectionString>(
    builder.Configuration.GetSection("ConnectionStrings"));

builder.Services.Configure<sqlCommandTimeout>(
    builder.Configuration.GetSection("sqlCommandTimeout"));
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

var Connectionstring = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrWhiteSpace(Connectionstring))
{
    throw new Exception("Database connection string is null or empty.");
}

var sinkOptions = new MSSqlServerSinkOptions
{
    TableName = "Logs",
    AutoCreateSqlTable = true
};

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.MSSqlServer(
        connectionString: Connectionstring,
        sinkOptions: sinkOptions)
    .CreateLogger();


Serilog.Debugging.SelfLog.Enable(msg =>
{
    Console.WriteLine($"Serilog SelfLog: {msg}");
});



var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
        RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
    };
});
builder.Services.AddAuthorization();
builder.Services.AddAutoMapper(typeof(MappingProfile));


builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddScoped< IDbConnectionFatory,DbConnectionFactory > ();
builder.Services.AddScoped<IUserReadRepository,UserReadRepository>();
builder.Services.AddScoped<IUserWriteRepository,UserWriteRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, JwtTokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRoleReadRepository, RoleReadRepository>();  
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<ICountryWriteRepository, CountryWriteRepository>();
builder.Services.AddScoped<ICountryReadRepository, CountryReadRepository>();


builder.Services.AddAuthorization();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer [your token]'"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddEndpointsApiExplorer();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseAuthentication ();

app.UseAuthorization();

app.MapControllers();

app.Run();
