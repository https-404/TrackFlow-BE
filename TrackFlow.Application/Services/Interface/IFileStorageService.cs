namespace TrackFlow.Application.IService;

public interface IFileStorageService
{
    Task UploadAsync(string objectName, Stream data, string contentType);
    Task<string> GetFileUrlAsync(string objectName);
    Task EnsureBucketExistsAsync();
}