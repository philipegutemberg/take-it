using System.Text;
using Application.DependencyInjection;
using Application.Settings;
using Domain.Injection;
using Ethereum.Nethereum.Injection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using MockDatabase.Injection;
using QRCode.BarCode.Injection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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
    .InjectMockDatabaseServices()
    .InjectNethereumServices(
        "https://summer-lingering-silence.ethereum-goerli.discover.quiknode.pro/ee2b11130f5ef374d83453df4e8adf8b2f70b840/", 
        "0edde832e34196546524378154c0643e387a23865b727395638de7afd6516225",
        "0xCa2acA0E413A6cbbC096F03E0896D28867f431b4",
        "2fe5e94432dbfe9cfc4334d54601f9106d69b330684cc0489ab053dfcbfdbbaf",
        "0xB051AFC251C1d18f7Db5D6E1e2b53dFbC73d7e41")
    .InjectBarcodeServices();

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