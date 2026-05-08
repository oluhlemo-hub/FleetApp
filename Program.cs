using FleetManagement.Services;
using Supabase;

var builder = WebApplication.CreateBuilder(args);

// Add Blazor
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Add Supabase — checks Railway env vars first, falls back to appsettings.json
var supabaseUrl = Environment.GetEnvironmentVariable("SUPABASE_URL") 
                  ?? builder.Configuration["Supabase:Url"]!;
var supabaseKey = Environment.GetEnvironmentVariable("SUPABASE_KEY") 
                  ?? builder.Configuration["Supabase:Key"]!;

builder.Services.AddScoped<Supabase.Client>(_ =>
    new Supabase.Client(supabaseUrl, supabaseKey, new SupabaseOptions
    {
        AutoRefreshToken = true,
        AutoConnectRealtime = false
    }));

builder.Services.AddScoped<FleetService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fleet Management API v1"));

app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.MapGet("/api", () => Results.Ok("Fleet Management API is running 🚀"));

app.Run();