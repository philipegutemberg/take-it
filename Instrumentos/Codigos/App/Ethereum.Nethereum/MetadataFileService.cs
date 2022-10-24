using System.Threading.Tasks;
using Domain.Models;
using Domain.Services.Interfaces;
using Ethereum.Nethereum.Metadata;
using Ethereum.Nethereum.Services.Interfaces;

namespace Ethereum.Nethereum
{
    internal class MetadataFileService : ITokenMetadataService
    {
        private readonly IFileStorageService _fileStorage;

        public MetadataFileService(IFileStorageService fileStorage)
        {
            _fileStorage = fileStorage;
        }

        public string Build(Event @event, EventTicketType eventType)
        {
            var metadataFile = new MetadataFile(@event, eventType);
            return metadataFile.ToJson();
        }

        public async Task<string> Save(string content, Event @event, EventTicketType eventType)
        {
            string key = $"{@event.Code}|{eventType.Code}";

            return await _fileStorage.SaveAndGetLink(key, content);
        }
    }
}