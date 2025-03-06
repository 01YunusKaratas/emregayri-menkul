using emregayrımenkul.Data;
using emregayrımenkul.Models;
using emregayrımenkul.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add DbContext service
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IAdvertService, AdvertService>();   //AddScope ile injection yaptık
builder.Services.AddScoped<IAdvertRepository, AdvertRepository>();//buradada aynı şekilde

//Explain for Identity


builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options =>
{
    // Şifre politikalarını buraya ekleyebilirsiniz
    options.Password.RequiredLength = 8;  // Minimum 8 karakter
    options.Password.RequireDigit = true;  // En az 1 rakam
    options.Password.RequireLowercase = true; // En az 1 küçük harf
    options.Password.RequireUppercase = true; // En az 1 büyük harf
    options.Password.RequireNonAlphanumeric = true; // En az 1 özel karakter
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
