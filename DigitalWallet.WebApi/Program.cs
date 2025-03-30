using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using DigitalWallet.Persistance.Context;
using Microsoft.AspNetCore.Identity;
using DigitalWallet.Application.Interfaces;
using DigitalWallet.Infrasturcture.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DigitalWallet.Persistance.Repositories;
using DigitalWallet.Domain.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddApplicationPart(typeof(AssemblyReference).Assembly);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();





//builder.Services.AddSwaggerGen(setup =>
//{
//    var jwtSecuritysheme = new OpenApiSecurityScheme
//    {
//        BearerFormat = "JWT",
//        Name = "JWT Authentication",
//        In = ParameterLocation.Header,
//        Type = SecuritySchemeType.Http,
//        Scheme = JwtBearerDefaults.AuthenticationScheme,
//        Description = "Put your JWT token",
//        Reference = new OpenApiReference
//        {
//            Id = JwtBearerDefaults.AuthenticationScheme,
//            Type = ReferenceType.SecurityScheme
//        }
//    };
//    setup.AddSecurityDefinition(jwtSecuritysheme.Reference.Id, jwtSecuritysheme);
//    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
//    {
//        { jwtSecuritysheme, Array.Empty<string>() }
//    });
//}
//);

builder.Services.AddSwaggerGen(setup =>
{
    var jwtSecuritysheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put your JWT token",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    setup.AddSecurityDefinition(jwtSecuritysheme.Reference.Id, jwtSecuritysheme);
    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecuritysheme, Array.Empty<string>() }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
    if (File.Exists(xmlPath))
    {
        setup.IncludeXmlComments(xmlPath);
    }


    var domainXmlFilename = "DigitalWallet.Domain.xml";
    var domainXmlPath = Path.Combine(AppContext.BaseDirectory, domainXmlFilename);
    if (File.Exists(domainXmlPath))
    {
        setup.IncludeXmlComments(domainXmlPath);
    }

    
    var applicationXmlFilename = "DigitalWallet.Application.xml";
    var applicationXmlPath = Path.Combine(AppContext.BaseDirectory, applicationXmlFilename);
    if (File.Exists(applicationXmlPath))
    {
        setup.IncludeXmlComments(applicationXmlPath);
    }

    
    var presentationXmlFilename = "DigitalWallet.Presentation.xml";
    var presentationXmlPath = Path.Combine(AppContext.BaseDirectory, presentationXmlFilename);
    if (File.Exists(presentationXmlPath))
    {
        setup.IncludeXmlComments(presentationXmlPath);
    }

    
    setup.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Digital Wallet API",
        Version = "v1",
        Description = "Digital Wallet uygulamasý için REST API"
  
    });
}
);



// JWT ayarlarýný oku ve servislere ekle
builder.Services.Configure<DigitalWallet.Application.Models.TokenOptions>(builder.Configuration.GetSection("Jwt"));

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IWalletService, WalletService>();
builder.Services.AddScoped<IWalletService, WalletService>();
builder.Services.AddScoped<IPaymentService, MockPaymentService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IWalletRepository, WalletRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();


var jwtSettings = builder.Configuration.GetSection("Jwt");
builder.Services.Configure<TokenOptions>(jwtSettings);

// Authentication ayarý
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173") 
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); 
    });
});



var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Digital Wallet API v1");
        c.RoutePrefix = "swagger";
    });
}   

//app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
