using app.Services;
using Coravel;
using app.Invocables;
using app.Models;
using app.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<InfluxDBService>();
builder.Services.AddTransient<WriteRandomPlaneAltitudeInvocable>();
builder.Services.AddScheduler();


var app = builder.Build();

app.Services.UseScheduler(scheduler =>
{
    scheduler
        .Schedule<WriteRandomPlaneAltitudeInvocable>()
        .EverySecond();
});
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
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
