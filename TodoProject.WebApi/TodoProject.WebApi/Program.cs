using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Identity;
using TodoProject.BusinessLayer.Abstract;
using TodoProject.BusinessLayer.Concrete;
using TodoProject.DataAccessLayer.Abstract;
using TodoProject.DataAccessLayer.Concrete;
using TodoProject.DataAccessLayer.EntityFramework;
using TodoProject.EntityLayer.Concrete;

using TodoProject.WebApi.Models.Jwt;
using TodoProject.WebApi.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(options =>
{
    options.ImplicitlyValidateChildProperties = true;
    options.ImplicitlyValidateRootCollectionElements = true;

    options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITodoDal,EFTodoDal>();
builder.Services.AddScoped<ITodoService,TodoManager>();

builder.Services.AddDbContext<Context>();

// entitylayer alt�ndaki concrete de bu ikisi olu�turuldu
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<Context>()
    .AddDefaultTokenProviders(); 


// identity �ifre al�rken art�k zorunluluk yok
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 5;
});




builder.Services.AddAuthentication(options => {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,  // yaz�lan issuer e�le�iyor mu
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? string.Empty))
            
        };
    });

// tum kontrollerlar appsettings'dekileri okuyabilsin
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));





builder.Services.AddAuthorization();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("TodoApiCors", opts =>
    {
        opts.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

// automapper
builder.Services.AddAutoMapper(typeof(Program));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// !!!!!!
// auth added -- Authorization �st�nde olmazsa �al��maz -- �nce yetkilendirme sonra yonlendirme
app.UseAuthentication();

app.UseAuthorization();



app.MapControllers();

app.Run();
