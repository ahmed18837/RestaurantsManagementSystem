using Microsoft.AspNetCore.Http;

namespace Restaurants.Application.Interfaces.Services
{
    public interface IFileService
    {
        string SaveFile(IFormFile? file, string category);
        Task DeleteFileAsync(string relativePath);
    }
}
