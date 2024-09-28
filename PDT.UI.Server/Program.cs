using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Cors.Infrastructure;
var builder = WebApplication.CreateBuilder(args);

// Enable serving static files
builder.Services.AddDirectoryBrowser();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllHeaders",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithExposedHeaders("Authorization");
        });
});
var app = builder.Build();

app.UseCors("AllowAllHeaders");
// Serve index.html as the root of the server
app.UseDefaultFiles(new DefaultFilesOptions
{
    DefaultFileNames = new List<string> { "index.html" },
    FileProvider = new PhysicalFileProvider(Path.Combine(AppContext.BaseDirectory, "PDT.UI"))
});
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(AppContext.BaseDirectory, "PDT.UI")),
    RequestPath = ""
});

app.Run();