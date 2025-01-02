using System.Net;
using Amazon.S3;
using Amazon.S3.Model;
using Logify.Extensions;
using Microsoft.Extensions.Logging;

namespace Axo.Shared.FileService.Service;

public class FileService(ILogger logger, IAmazonS3 s3Client) : IFileService
{
  public async Task<string?> SaveFileToS3(Stream content, string fileName)
  {
    var bucketName = Environment.GetEnvironmentVariable("awsBucketName");

    var objectRequest = new PutObjectRequest
    {
      BucketName = bucketName,
      Key = fileName,
      InputStream = content
    };

    try
    {
      var putRequest = await s3Client.PutObjectAsync(objectRequest);

      return putRequest.HttpStatusCode != HttpStatusCode.OK ? null : fileName;
    }
    catch (Exception e)
    {
      logger.LogErrorCustom("Failed to upload file to S3.", e);
      return null;
    }
  }

  public async Task<Stream?> GetContentFileS3(string filePath)
  {
    var bucketName = Environment.GetEnvironmentVariable("awsBucketName");

    var objectRequest = new GetObjectRequest
    {
      BucketName = bucketName,
      Key = filePath
    };

    try
    {
      var response = await s3Client.GetObjectAsync(objectRequest);

      var memoryStream = new MemoryStream();
      await response.ResponseStream.CopyToAsync(memoryStream);
      memoryStream.Position = 0;

      return memoryStream;
    }
    catch (Exception e)
    {
      logger.LogErrorCustom("Failed to get file from S3.", e);
      return null;
    }
  }
  
  public async Task<bool> RemoveFileFromS3(string filePath)
  {
    var bucketName = Environment.GetEnvironmentVariable("awsBucketName");

    var objectRequest = new DeleteObjectRequest
    {
      BucketName = bucketName,
      Key = filePath
    };

    try
    {
      var deleteRequest = await s3Client.DeleteObjectAsync(objectRequest);

      if (deleteRequest.HttpStatusCode == HttpStatusCode.OK) return true;
      
      logger.LogInformationCustom("Failed to delete file from S3.");
      return false;

    }
    catch (Exception e)
    {
      logger.LogErrorCustom("Failed to delete file from S3.", e);
      return false;
    }
  }

  public Stream GenerateFile(byte[] content)
  {
    var memoryStream = new MemoryStream(content);

    return memoryStream;
  }
}