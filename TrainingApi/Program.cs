using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    
})
    .AddJwtBearer(x =>
    {
        x.MetadataAddress = "http://auth.hypertheory.cloud/auth/realms/hypertheory/.well-known/openid-configuration";
        x.RequireHttpsMetadata = false; // only for development
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidAudience = "front-end",
            ValidateIssuer = true,
            ValidIssuer = "http://auth.hypertheory.cloud/auth/realms/Hypertheory",
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.FromMinutes(1)

        };
       
    });



builder.Services.AddCors(builder =>
{
    builder.AddDefaultPolicy(builder =>
    {
        builder.AllowCredentials();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
        builder.WithOrigins("http://localhost:4200");
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    IdentityModelEventSource.ShowPII = true; // This is debug stuff for keycloak
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
