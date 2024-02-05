using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.IO;
using System.Threading.Tasks;
public partial class Program
{

    // 非同期のMainメソッドもサポートされています (C# 7.1 以降)
    public static async Task Main(string[] args)
    {

        // See https://aka.ms/new-console-template for more information
        Console.WriteLine("Hello, World!");

        await test1Async();
    }


    static async Task test1Async()
    {
        // 接続文字列の取得
        string connectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING", EnvironmentVariableTarget.User);

        if (connectionString == null)
        {
            Console.WriteLine($"エラー: AZURE_STORAGE_CONNECTION_STRINGがない");
            Environment.Exit(1);
        }

        BlobService.BlobService blobService = new BlobService.BlobService(connectionString);
        // 既存のコンテナに接続
        string existingContainerName = "quickstartblobs7489e86e-5ab5-403f-8ea4-7adbbad87d00";
        blobService.GetContainerClient(existingContainerName);

        // Blobのリストアップ
        await blobService.ListBlobsAsync();

        // Blobのアップロード
        // Create a local file in the ./data/ directory for uploading and downloading
        string localPath = "data";
        Directory.CreateDirectory(localPath);
        string fileName = "quickstart" + Guid.NewGuid().ToString() + ".txt";

        string localFilePath = Path.Combine(localPath, fileName);

        // Write text to the file
        await File.WriteAllTextAsync(localFilePath, "Hello, World!");
        await blobService.UploadFileAsync(localFilePath, fileName);

        // Blobのダウンロード
        string downloadFilePath = localFilePath.Replace(".txt", "DOWNLOADED.txt");
        await blobService.DownloadFileAsync(fileName, downloadFilePath);


        Console.WriteLine("Done");
    }
}