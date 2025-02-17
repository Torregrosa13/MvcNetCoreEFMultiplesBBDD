using Microsoft.EntityFrameworkCore;
using MvcNetCoreEFMultiplesBBDD.Data;
using MvcNetCoreEFMultiplesBBDD.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//string connectionString = builder.Configuration.GetConnectionString("SqlHospital");
//builder.Services.AddTransient<IRepositoryEmpleados ,RepositoryEmpleados>();
//builder.Services.AddDbContext<HospitalContext>(options => options.UseSqlServer(connectionString));


//string connectionString = builder.Configuration.GetConnectionString("OracleHospital");
//builder.Services.AddTransient<IRepositoryEmpleados ,RepositoryEmpleadosOracle>();
//builder.Services.AddDbContext<HospitalContext>
//(options => options.UseOracle(connectionString));

string connectionString = builder.Configuration.GetConnectionString("MySqlHospital");
builder.Services.AddTransient<IRepositoryEmpleados, RepositoryEmpleadosMySql>();
builder.Services.AddDbContext<HospitalContext>
(options => options.UseMySQL(connectionString));

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
