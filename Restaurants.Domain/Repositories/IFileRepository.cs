using Microsoft.AspNetCore.Http;

namespace Restaurants.Domain.Repositories
{
    public interface IFileRepository
    {
        string SaveFile(IFormFile? file, string category);
        Task DeleteFileAsync(string relativePath);
    }
}
