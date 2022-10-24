using System.Security.Cryptography;
using System.Text;

namespace Application.Services
{
    public class HashService
    {
        public string GetHash(string text)
        {
            StringBuilder Sb = new();

            using (var hash = SHA256.Create())
            {
                byte[] result = hash.ComputeHash(Encoding.UTF8.GetBytes(text));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }
    }
}