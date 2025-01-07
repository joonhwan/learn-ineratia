using InertiaCore.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddInertia(o =>
{
    // o.RootView = typeof(HelloInertia.Views.Home.Index);
});
builder.Services.AddViteHelper(o =>
{
    o.PublicDirectory = "wwwroot";
    o.BuildDirectory = "build";
    o.ManifestFilename = "manifest.json";
});

var app = builder.Build();

app.UseInertia();

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

//app.MapFallbackToFile("index.html");

app.Run();