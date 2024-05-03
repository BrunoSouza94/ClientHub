namespace ClientHub.Infrastructure.Image;

public class AzureOptions
{
    public string ContainerName { get; init; } = string.Empty;
    public string StorageAccountAzureConnectionString { get; init; } = string.Empty;
}