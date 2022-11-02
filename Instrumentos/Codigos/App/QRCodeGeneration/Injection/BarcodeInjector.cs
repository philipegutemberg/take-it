using Domain.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace QRCodeGeneration.Injection
{
    public static class BarcodeInjector
    {
        public static IServiceCollection InjectBarcodeServices(this IServiceCollection services) => services
            .AddTransient<IQRCodeService, BarCodeQRCodeService>();
    }
}