using FleetManagement.Services;
using Supabase;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

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

// Allow SignalR from any origin (needed for Railway)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fleet Management API v1"));

app.UseStaticFiles();
app.UseCors();
app.UseWebSockets();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.MapGet("/api", () => Results.Ok("Fleet Management API is running 🚀"));

app.Run();