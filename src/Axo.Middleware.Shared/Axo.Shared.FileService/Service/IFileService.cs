namespace Axo.Shared.FileService.Service;

public interface IFileService
{
  Task<string> SaveFileToS3(Stream content, string fileName);
  Task<Stream> GetContentFileS3(string filePath);
  Task<bool> RemoveFileFromS3(string filePath);
  Stream GenerateFile(byte[] content);
}