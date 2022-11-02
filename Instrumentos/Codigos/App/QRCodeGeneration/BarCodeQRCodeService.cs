using System.IO;
using System.Threading.Tasks;
using Domain.Services.Interfaces;
using SkiaSharp;
using SkiaSharp.QrCode.Image;

namespace QRCodeGeneration
{
    internal class BarCodeQRCodeService : IQRCodeService
    {
        public Task<byte[]> Generate(string text)
        {
            using var stream = new MemoryStream();

            var qrCode = new QrCode(text, new Vector2Slim(512, 512), SKEncodedImageFormat.Png);

            qrCode.GenerateImage(stream);

            return Task.FromResult(stream.ToArray());
        }
    }
}