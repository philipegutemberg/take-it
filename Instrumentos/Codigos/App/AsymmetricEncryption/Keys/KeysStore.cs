using System.IO;

namespace AsymmetricEncryption.Keys
{
    internal class KeysStore
    {
        private readonly string _pem;

        public KeysStore(string pemFilePath)
        {
            _pem = File.ReadAllText(pemFilePath);
        }

        public string GetPEM() => _pem;
    }
}