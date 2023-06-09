using DonaMenina.Context;
using DonaMenina.Helper;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataBaseContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("StandardConection")));

builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<DonaMenina.Helper.IUserSession, Session>();
builder.Services.AddScoped<DonaMenina.Helper.ISaleSession, SaleSession>();

builder.Services.AddSession(o =>
{
    o.Cookie.HttpOnly= true;
    o.Cookie.IsEssential= true;
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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
