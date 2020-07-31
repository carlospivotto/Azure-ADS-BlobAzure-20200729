using System.IO;
using System;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.Threading.Tasks;

namespace BlobAzure
{
    class Program
    {
        static async Task Main()
        {
            
            Console.WriteLine("Exemplo com Blobs no Azure Storage.");
            string connectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");

            //Criar um contêiner:
            //Nomes de contêineres devem ser totalmente caixa baixa (minúsculas)
            
            //Passos para upload do blob no contêiner já criado:
            //1. Criar um BlobServiceClient - será usado para criar um cliente de contêiner
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

            //2. Batize o seu contêiner:
            //string container = "blobazure" + Guid.NewGuid().ToString();
            string container = "exemplo-aula-2020-07-29";

            //3. Criar um BlobContainerClient - um cliente para o contêiner:
            BlobContainerClient blobContainerClient = await blobServiceClient.CreateBlobContainerAsync(container);

            //4. Criar ou referenciar um arquivo
            string localPath = "D:/";
            string fileName = "git.pdf";
            string localFilePath = Path.Combine(localPath, fileName);

            // 5. Montar uma referência para este blob:
            BlobClient blobClient = blobContainerClient.GetBlobClient(fileName);

            Console.WriteLine($"Realizando upload para: {blobClient.Uri}...");

            //6. Realizar o upload propriamente dito:
            using FileStream upload = File.OpenRead(localFilePath);
            await blobClient.UploadAsync(upload, true);
            upload.Close();

        }
    }
}