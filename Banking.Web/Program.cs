using Microsoft.AspNetCore.Http.Headers;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseMigrationsEndPoint();
}
else
{
  app.UseExceptionHandler("/Home/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseRouting();

app.UseEndpoints(endpoints =>
{
  endpoints.MapDefaultControllerRoute();
});

if (app.Environment.IsDevelopment())
{
  app.UseSpa(spa =>
  {
    spa.UseProxyToSpaDevelopmentServer("https://localhost:5173");
  });
}
else
{
  app.UseSpaStaticFiles();
  app.UseSpa(spa =>
  {
    spa.Options.SourcePath = "Client/dist";

    // adds no-store header to index page to prevent deployment issues (prevent linking to old .js files)
    // .js and other static resources are still cached by the browser
    spa.Options.DefaultPageStaticFileOptions = new StaticFileOptions
    {
      OnPrepareResponse = ctx =>
      {
        ResponseHeaders headers = ctx.Context.Response.GetTypedHeaders();
        headers.CacheControl = new CacheControlHeaderValue
        {
          NoCache = true,
          NoStore = true,
          MustRevalidate = true
        };
      }
    };
  });
}

app.Run();
