using TodoProject.BusinessLayer.Abstract;
using TodoProject.BusinessLayer.Concrete;
using TodoProject.DataAccessLayer.Abstract;
using TodoProject.DataAccessLayer.Concrete;
using TodoProject.DataAccessLayer.EntityFramework;
using TodoProject.EntityLayer.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITodoDal,EFTodoDal>();
builder.Services.AddScoped<ITodoService,TodoManager>();

builder.Services.AddDbContext<Context>();

// entitylayer altýndaki concrete de bu ikisi oluþturuldu
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
