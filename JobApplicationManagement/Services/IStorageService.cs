using Amazon.Runtime;
using JobApplicationManagement.config;
using JobApplicationManagement.Models;

namespace JobApplicationManagement.Services
{
    public interface IStorageService
    {
        Task<S3ResponseDto> UploadFileAsync(string name, MemoryStream inputStream, string bucketName);
        Task<S3ResponseDto> DownloadFileAsync(string name, MemoryStream inputStream, string bucketName);
        Task<IFormFile> GetContentFile(
           string name,
           string bucketName);
        string getFullPathFile(string filePath);
    }
}
