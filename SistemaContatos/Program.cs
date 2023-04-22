using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SistemaContatos.Data;
using SistemaContatos.Interfaces;
using SistemaContatos.Repository;

var builder = WebApplication.CreateBuilder(args);

string conn = builder.Configuration.GetConnectionString(name: "DataBase");

builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer((conn) ?? throw new InvalidOperationException("Connection string 'SistemaContatos' not found.")));
//Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddScoped<IContatoRepository, ContatoRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
