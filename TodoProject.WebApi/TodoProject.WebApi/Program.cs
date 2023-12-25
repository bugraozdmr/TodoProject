using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;
using TodoProject.BusinessLayer.Abstract;
using TodoProject.BusinessLayer.Concrete;
using TodoProject.DataAccessLayer.Abstract;
using TodoProject.DataAccessLayer.Concrete;
using TodoProject.DataAccessLayer.EntityFramework;
using TodoProject.EntityLayer.Concrete;
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

// entitylayer altındaki concrete de bu ikisi oluşturuldu
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<Context>();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("TodoApiCors", opts =>
    {
        opts.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
