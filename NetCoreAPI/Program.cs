using NetCoreAPI.Mapping;
using NetCoreAPI.Middlewares;
using NetCoreAPI.Repository;
using NetCoreAPI.Services;
using Microsoft.OpenApi.Models;
using NetCoreAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// singleton olarak ekliyoruz → uygulama boyunca aynı instance kalır
builder.Services.AddSingleton<CampaignRepository>();
builder.Services.AddSingleton<CampaingService>();
builder.Services.AddSingleton<TokenService>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//builder.WebHost.UseUrls("http://0.0.0.0:8080");

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "POC API", Version = "v1" });
    c.EnableAnnotations();
    c.TagActionsBy(api =>
    {
        return new[] { "Kampanya İşlemleri RestAPI" };
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Token giriniz. Örnek: Bearer {token}"
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
            Array.Empty<string>()
        }
    });
});

var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();

builder.Services.AddSingleton(jwtSettings);

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwtSettings!.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.Key))
        };
    });

 
 
var app = builder.Build();
 
app.UseMiddleware<EndPointMiddleWare>();


app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();
 
//JWT için Eklendi
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

