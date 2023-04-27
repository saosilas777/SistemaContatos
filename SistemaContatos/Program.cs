using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SistemaContatos.Data;
using SistemaContatos.Helper;
using SistemaContatos.Interfaces;
using SistemaContatos.Repository;
using SistemaContatos.Services.SendFileServices;

var builder = WebApplication.CreateBuilder(args);

string conn = builder.Configuration.GetConnectionString(name: "DataBase");

builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer((conn) ?? throw new InvalidOperationException("Connection string 'SistemaContatos' not found.")));
//Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddScoped<IContatoRepository, ContatoRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISendEmail, SendEmail>();
builder.Services.AddSingleton<ISection, Section>();
builder.Services.AddScoped<SendFileServices>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();


builder.Services.AddSession(obj =>
{
	obj.Cookie.HttpOnly = true;
	obj.Cookie.IsEssential = true;
});


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
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
