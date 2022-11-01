using System;
using System.Security.Cryptography;
using System.Text;
using AsymmetricEncryption.Keys;
using Domain.Services.Interfaces;

namespace AsymmetricEncryption
{
    internal class AsymmetricEncryptionService : IEncryptionService
    {
        private readonly RSACryptoServiceProvider _rsa;

        public AsymmetricEncryptionService(KeysStore keysStore)
        {
            _rsa = new RSACryptoServiceProvider(2048);
            _rsa.ImportFromPem(keysStore.GetPEM());
        }

        public string Encrypt(string text)
        {
            var textBytes = Encoding.UTF8.GetBytes(text);
            var cypherBytes = _rsa.Encrypt(textBytes, true);
            return Convert.ToBase64String(cypherBytes);
        }

        public string Decrypt(string encryptedText)
        {
            var cypherBytes = Convert.FromBase64String(encryptedText);
            var textBytes = _rsa.Decrypt(cypherBytes, true);
            return Encoding.UTF8.GetString(textBytes);
        }
    }
}