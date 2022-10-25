using Project.Service.Interfaces.IVehicleRepository;
using Project.Service.Interfaces.IVehicleService;
using Project.Service.VehicleService;
using ZaPrav.NetCore.VehicleDB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IVehicleServiceModel, VehicleServiceModel>();
builder.Services.AddTransient<IVehicleRepositoryModel, VehicleRepositoryModel>();
builder.Services.AddTransient<IVehicleServiceMake, VehicleServiceMake>();
builder.Services.AddTransient<IVehicleRepositoryMake, VehicleRepositoryMake>();
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
    pattern: "{controller=Make}/{action=VehicleMake}/{id?}");

app.Run();
