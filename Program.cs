using library_system.Business;
using library_system.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddDistributedMemoryCache();  // Required for session

builder.Services.AddSession();

builder.Services.AddSession(options =>

{

    options.IdleTimeout = TimeSpan.FromDays(30);

    options.Cookie.HttpOnly = true;

    options.Cookie.IsEssential = true; // needed if you have cookie consent

});



builder.Services.ConfigureApplicationCookie(options =>
{
    // Configure the cookie to be persistent
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(30); // The cookie will expire in 30 days
    options.SlidingExpiration = true; // The cookie lifetime is extended on each request
});

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<AuthenticationBO>();
builder.Services.AddScoped<AuthorBO>();
builder.Services.AddScoped<BookBO>();
builder.Services.AddScoped<ClientBO>();
builder.Services.AddScoped<PubblisherBO>();
builder.Services.AddScoped<BorrowBO>();



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
app.UseSession();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Clients}/{action=Indexlog}/{id?}")

    ;


app.Run();
