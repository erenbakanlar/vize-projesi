using odev.dagitim.portali.repositories;
using odev.dagitim.portali.repositories;
using Microsoft.EntityFrameworkCore;
using odev.dagitim.portali.data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "MyCookieAuth";
    options.DefaultChallengeScheme = "MyCookieAuth";
})
.AddCookie("MyCookieAuth", options =>
{
    options.LoginPath = "/Admin/Login/Index";
});


builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddScoped<IOgrenciRepository, OgrenciRepository>();
builder.Services.AddScoped<IOdevRepository, OdevRepository>();
builder.Services.AddScoped<IDagitilanOdevRepository, DagitilanOdevRepository>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();        

app.UseSession();        
app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();

