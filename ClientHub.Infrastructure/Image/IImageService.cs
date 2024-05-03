using Microsoft.AspNetCore.Http;

namespace ClientHub.Infrastructure.Image;

public interface IImageService
{
    Task<string> UploadImageAsync(Guid id, string base64File, string fileName);
}