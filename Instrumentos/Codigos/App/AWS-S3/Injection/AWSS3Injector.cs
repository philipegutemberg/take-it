using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.S3;
using Ethereum.Nethereum.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AWS_S3.Injection
{
    public static class AWSS3Injector
    {
        public static IServiceCollection InjectS3Services(
            this IServiceCollection services,
            string accessKey,
            string secret,
            RegionEndpoint regionEndpoint) => services
            .AddDefaultAWSOptions(new AWSOptions
            {
                Credentials = new BasicAWSCredentials(accessKey, secret),
                Region = regionEndpoint
            })
            .AddAWSService<IAmazonS3>()
            .AddTransient<IFileStorageService, S3FileService>();
    }
}