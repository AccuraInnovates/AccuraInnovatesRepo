using Accura_Innovatives.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
string connection = builder.Configuration.GetConnectionString("DefaultConnection") ??
    throw new InvalidOperationException("Connection String 'constring' not found");
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<EmployeeMaster1Context>(options =>
{
    options.UseSqlServer(connection);
});

builder.Services.AddCloudscribePagination();
//builder.Services.AddControllersWithViews().AddJsonOptions(o =>
//{
//    o.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
//    o.JsonSerializerOptions.PropertyNamingPolicy = null;

//});
var app = builder.Build();
//builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
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
public partial class Program
{
    public static void Main()
    {
        // Register the 1252 encoding provider
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        // The rest of your application code...
    }
}