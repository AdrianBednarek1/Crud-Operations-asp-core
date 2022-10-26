using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Project.Service.Interfaces.IVehicleRepository;
using Project.Service.Interfaces.IVehicleService;
using Project.Service.VehicleService;
using ZaPrav.NetCore.VehicleDB;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddTransient<IVehicleServiceMake, VehicleServiceMake>();
builder.Services.AddTransient<IVehicleServiceModel, VehicleServiceModel>();
builder.Services.AddTransient<IVehicleRepositoryMake, VehicleRepositoryMake>();
builder.Services.AddTransient<IVehicleRepositoryModel, VehicleRepositoryModel>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
