using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BlobFunction
{
    public static class BlobHttpTrigger
    {
        // Blobサービスクライアントの初期化
        private static readonly BlobServiceClient BlobServiceClient = new BlobServiceClient(Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING"));
        // private static readonly BlobContainerClient ContainerClient = BlobServiceClient.GetBlobContainerClient("quickstartblobs7489e86e-5ab5-403f-8ea4-7adbbad87d00");
        private static readonly BlobContainerClient ContainerClient = BlobServiceClient.GetBlobContainerClient("nobel-script");


        [FunctionName("BlobHttpTriggerFunc")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            log.LogInformation(req.Method);

            // POSTリクエストの場合（ファイルアップロード）
            if (req.Method == HttpMethods.Post)
            {
                //var file = req.Form.Files["file"];
                var file = req.Form.Files[0];
                if (file == null)
                {
                    return new BadRequestObjectResult("File is missing");
                }

                var blobClient = ContainerClient.GetBlobClient(file.FileName);
                using (var stream = file.OpenReadStream())
                {
                    await blobClient.UploadAsync(stream, overwrite: true);
                }
                return new OkObjectResult($"File uploaded successfully: {file.FileName}");
            }

            // GETリクエストの場合（ファイルダウンロード）
            else if (req.Method == HttpMethods.Get)
            {
                string fileName = req.Query["filename"];
                log.LogInformation(fileName);
                if (string.IsNullOrEmpty(fileName))
                {
                    return new BadRequestObjectResult("Please provide a filename");
                }

                var blobClient = ContainerClient.GetBlobClient(fileName);
                if (await blobClient.ExistsAsync())
                {
                    var blobDownloadInfo = await blobClient.DownloadAsync();

                    return new FileStreamResult(blobDownloadInfo.Value.Content, blobDownloadInfo.Value.ContentType)
                    {
                        FileDownloadName = fileName
                    };
                }
                else
                {
                    return new NotFoundObjectResult("File not found");
                }
            }

            return new BadRequestObjectResult("Request is neither GET nor POST");
        }
    }
}
