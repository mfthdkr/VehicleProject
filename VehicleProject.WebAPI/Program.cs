using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using VehicleProject.CoreLayer.Configurations;
using VehicleProject.CoreLayer.Entities.Concrete;
using VehicleProject.CoreLayer.Extensions;
using VehicleProject.CoreLayer.Repositories;
using VehicleProject.CoreLayer.Services;
using VehicleProject.CoreLayer.Services.Abstract;
using VehicleProject.CoreLayer.UnitOfWork;
using VehicleProject.DataAccessLayer.Context;
using VehicleProject.DataAccessLayer.Repositories;
using VehicleProject.DataAccessLayer.UnitOfWork;
using VehicleProject.ServiceLayer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(options =>
{
    // ilgili assemmlydeki bütün validation kurallarýný uygular.
    options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
});

// Fluent Validation hatalarýný gösterdiði default  sýnýfý ezip kendimiz ErrorDto dönen extension yazdýk.
builder.Services.UseCustomValidationResponse();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Swagger UI'da jwt yapýlandýrmasý
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "VehicleProject.WebAPI", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                });
});


// DI Register
builder.Services.AddScoped<IBoatRepository, BoatRepository>();
builder.Services.AddScoped<IBusRepository, BusRepository>();
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<IColorRepository, ColorRepository>();

builder.Services.AddScoped<IBoatService, BoatService>();
builder.Services.AddScoped<IBusService, BusService>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IColorService, ColorService>();

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IGenericService<,>), typeof(GenericService<,>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// DbContext
builder.Services.AddDbContext<VehicleProjectContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"), sqlOptions =>
    {
        sqlOptions.MigrationsAssembly(Assembly.GetAssembly(typeof(VehicleProjectContext)).ToString());
       
    });
});

// Identity Üyelik Sistemi
builder.Services.AddIdentity<User, IdentityRole>(Opt =>
{
    Opt.User.RequireUniqueEmail = true;
    Opt.Password.RequireNonAlphanumeric = false;
}).AddEntityFrameworkStores<VehicleProjectContext>()
            .AddDefaultTokenProviders(); 

// Token üretmek için ilgili bilgileri alýr.
builder.Services.Configure<CustomTokenOption>(builder.Configuration.GetSection("TokenOption"));

// Gelen token'ýn doðruluðunu kabul eder.
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts =>
{
    var tokenOptions = builder.Configuration.GetSection("TokenOption").Get<CustomTokenOption>();
    opts.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidIssuer = tokenOptions.Issuer,
        ValidAudience = tokenOptions.Audience[0],
        IssuerSigningKey = SignService.GetSymmetricSecurityKey(tokenOptions.SecurityKey),

        ValidateIssuerSigningKey = true,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "VehicleProject.WebAPI v1"));
}

// custom exception middleware
app.UseCustomException();

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
