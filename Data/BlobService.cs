using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;

namespace Husyoudaddy.Data
{
    public class BlobService
    {
        private BlobServiceClient client;
        public BlobService(IConfiguration configation)
        {
            string connectionString = configation["BlobService:ConnectionString"];
            string AccountKey = configation["tenant-asa-key"];
            client = new BlobServiceClient(connectionString + AccountKey);
        }

        public async Task<string> getScenarioCode(string tenant, string id)
        {
            BlobContainerClient containerClient = client.GetBlobContainerClient("scenarios");
            BlobClient blobClient = containerClient.GetBlobClient(tenant + "/" + id);
            BlobDownloadInfo download = await blobClient.DownloadAsync();
            StreamReader reader = new StreamReader(download.Content);
            string s = await reader.ReadToEndAsync();
            return s;
        }
    }
}
