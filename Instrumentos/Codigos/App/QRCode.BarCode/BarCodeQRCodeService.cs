using System.IO;
using System.Text;
using System.Threading.Tasks;
using Domain.Services.Interfaces;
using Net.Codecrete.QrCodeGenerator;

namespace QRCode.BarCode
{
    internal class BarCodeQRCodeService : IQRCodeService
    {
        public Task<byte[]> Generate(string text)
        {
            var qr = QrCode.EncodeText(text, QrCode.Ecc.Medium);
            return Task.FromResult(qr.ToPng(10, 0));
        }
    }
}