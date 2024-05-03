using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace ClientHub.Infrastructure.Image;

public class ImageService : IImageService
{ 
    private readonly AzureOptions _azureOptions;

    public ImageService(IOptions<AzureOptions> azureOptions)
    {
        _azureOptions = azureOptions.Value;
    }

    public async Task<string> UploadImageAsync(Guid id, string base64File, string fileName)
    {
        var serviceClient = new BlobServiceClient(_azureOptions.StorageAccountAzureConnectionString);
        var containerClient = serviceClient.GetBlobContainerClient(_azureOptions.ContainerName);
        var blobClient = containerClient.GetBlobClient(id.ToString() + fileName);

        var imageBytes = Convert.FromBase64String(base64File);
        using MemoryStream stream = new(imageBytes);

        await blobClient.UploadAsync(stream, new BlobUploadOptions
        {
            AccessTier = AccessTier.Cool
        });

        return blobClient.Uri.ToString();
    }
}