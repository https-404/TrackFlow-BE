using Minio;
using Minio.DataModel.Args;
using TrackFlow.Application.IService;
using TrackFlow.Infrastructure.Setting;

namespace TrackFlow.Application.Services.Implementation;

public class FileStorageService(MinioClient minio, MinioSettings settings) : IFileStorageService
{
    public async Task UploadAsync(string objectName, Stream data, string contentType)
        {
            await minio.PutObjectAsync(new PutObjectArgs()
                .WithBucket(settings.BucketName)
                .WithObject(objectName)
                .WithStreamData(data)
                .WithObjectSize(data.Length)
                .WithContentType(contentType));
        }

        public async Task<string> GetFileUrlAsync(string objectName)
        {
            var url = await minio.PresignedGetObjectAsync(
                new PresignedGetObjectArgs()
                    .WithBucket(settings.BucketName)
                    .WithObject(objectName)
                    .WithExpiry(3600));
            return url;
        }

        public async Task EnsureBucketExistsAsync()
        {
            bool found = await minio.BucketExistsAsync(
                new BucketExistsArgs().WithBucket(settings.BucketName));

            if (!found)
            {
                await minio.MakeBucketAsync(
                    new MakeBucketArgs().WithBucket(settings.BucketName));
            }
        }
}
