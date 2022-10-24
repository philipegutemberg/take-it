using System.IO;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Ethereum.Nethereum.Services.Interfaces;

namespace AWS_S3
{
    internal class S3FileService : IFileStorageService
    {
        private readonly IAmazonS3 _amazonS3;

        public S3FileService(IAmazonS3 amazonS3)
        {
            _amazonS3 = amazonS3;
        }

        public async Task<string> SaveAndGetLink(string key, string content)
        {
            string bucketName = "take-it"; /* ToDo: receber bucket name de algum lugar */
            string address = $"metadata-files/{key}.json";

            await _amazonS3.PutObjectAsync(new PutObjectRequest
            {
                BucketName = bucketName,
                Key = address,
                InputStream = await GetFileStream(content)
            });

            return $"https://{bucketName}.s3.amazonaws.com/{address}";
        }

        private async Task<Stream> GetFileStream(string content)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);

            await writer.WriteAsync(content);
            writer.Flush();

            stream.Position = 0;
            return stream;
        }
    }
}