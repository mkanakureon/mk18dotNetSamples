using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BlobService
{
    public class BlobService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private BlobContainerClient _containerClient;

        public BlobService(string connectionString)
        {
            if (connectionString!=null) {
                _blobServiceClient = new BlobServiceClient(connectionString);
            }
        }


        public void GetContainerClient(string containerName)
        {
            _containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        }

        public async Task CreateContainerAsync(string containerName)
        {
            _containerClient = await _blobServiceClient.CreateBlobContainerAsync(containerName);
        }

        public async Task UploadFileAsync(string localFilePath, string fileName)
        {
            BlobClient blobClient = _containerClient.GetBlobClient(fileName);
            Console.WriteLine($"Uploading to Blob storage as blob:\n\t {blobClient.Uri}\n");
            await blobClient.UploadAsync(localFilePath, true);
        }

        public async Task DownloadFileAsync(string blobName, string downloadFilePath)
        {
            BlobClient blobClient = _containerClient.GetBlobClient(blobName);
            Console.WriteLine($"\nDownloading blob to\n\t{downloadFilePath}\n");
            await blobClient.DownloadToAsync(downloadFilePath);
        }

        public async Task ListBlobsAsync()
        {
            Console.WriteLine("Listing blobs...");
            await foreach (BlobItem blobItem in _containerClient.GetBlobsAsync())
            {
                Console.WriteLine("\t" + blobItem.Name);
            }
        }

        public async Task DeleteContainerAsync()
        {
            Console.WriteLine("Deleting blob container...");
            await _containerClient.DeleteAsync();
        }
    }
}
