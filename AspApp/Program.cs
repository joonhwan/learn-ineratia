using AspApp;
using AspApp.Services;
using InertiaCore;
using InertiaCore.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddJsonPlaceholderService();
builder.Services.AddControllersWithViews();
builder.Services.AddInertia(o =>
{
    o.RootView = "~/Views/App.cshtml";
});
builder.Services.AddViteHelper(o =>
{
    o.PublicDirectory = "wwwroot";
    o.BuildDirectory = "build";
    o.ManifestFilename = "manifest.json";
});

var app = builder.Build();

app.UseInertia();
app.Use((context, next) => {
    //context.Response.Headers.Append("X-Frame-Options", "SAMEORIGIN");
    Inertia.Share("auth", new
    {
        User = new
        {
            Name = "John Doe",
            Email = "john@example.com",
        }
    });
    return next();
});

// Configure the HTTP request pipeline.
// app.UseExceptionHandler("/Error");  
app.UseMiddleware<InertiaErrorPageMiddleware>();

if (app.Environment.IsDevelopment())
{
    // app.UseStatusCodePages();
    app.UseDeveloperExceptionPage();
}
else
{
    //app.UseExceptionHandler("/Error");
    app.UseStatusCodePagesWithReExecute("/Error/{0}");
    
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
//app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.MapFallbackToFile("index.html");

app.Run();