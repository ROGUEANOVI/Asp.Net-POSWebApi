using Microsoft.AspNetCore.Http;

namespace POS.Infrastructure.FileStorage
{
    public interface IAzureStorage
    {
        Task<string> SaveFile(string container, IFormFile file);
        Task<string> EditFile(string route, string container, IFormFile file);
        Task RemoveFile(string route, string container);
    }
}
