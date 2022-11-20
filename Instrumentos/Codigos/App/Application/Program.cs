using System.Text;
using System.Text.Json.Serialization;
using Amazon;
using Application.BackgroundWorker;
using Application.Injection;
using Application.Settings;
using AsymmetricEncryption.Injection;
using AWS_S3.Injection;
using Database.SQLServer.Injection;
using Domain.Injection;
using Ethereum.Nethereum.Injection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((_, loggerConfiguration) =>
{
    loggerConfiguration
        .WriteTo.Console(theme: Serilog.Sinks.SystemConsole.Themes.AnsiConsoleTheme.Code)
        .Enrich.FromLogContext();
});

// Add services to the container.
builder.Services
    .AddControllers()
    .AddJsonOptions(opts =>
    {
        var enumConverter = new JsonStringEnumConverter();
        opts.JsonSerializerOptions.Converters.Add(enumConverter);
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    var key = Encoding.ASCII.GetBytes(TokenSettings.Secret);

    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services
    .InjectDomainServices()
    .InjectApplicationServices()
    .InjectSqlServerServices()
    .InjectNethereumServices(
        $"https://summer-lingering-silence.ethereum-goerli.discover.quiknode.pro/ee2b11130f5ef374d83453df4e8adf8b2f70b840/",
        "ripple scissors kick mammal hire column oak again sun offer wealth tomorrow wagon turn fatal",
        "C@78726fFhd$Bj8")
    .InjectS3Services("AKIAW7G7XFRODL3YN5UO", "n0bSUIdqLQeMm+xIMiKXl0X8mwPM2q4U8DlL8nOD", RegionEndpoint.USEast1)
    .InjectAsymmetricEncryptionServices("keys.pem");

builder.Services
    .AddHostedService<BlockchainEventsProcessorWorker>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();