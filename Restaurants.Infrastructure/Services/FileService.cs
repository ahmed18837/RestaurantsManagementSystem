using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Restaurants.Application.Interfaces.Services;

namespace Restaurants.Infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _baseUploadPath;
        private readonly string _webRootPath;

        public FileService(IWebHostEnvironment environment, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _webRootPath = environment.WebRootPath ?? Path.Combine(environment.ContentRootPath, "wwwroot");
            _baseUploadPath = Path.Combine(_webRootPath, "Images");

            if (!Directory.Exists(_baseUploadPath))
            {
                Directory.CreateDirectory(_baseUploadPath);
            }
        }

        public string SaveFile(IFormFile? file, string category)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(file!.FileName).ToLower();
            if (!allowedExtensions.Contains(extension))
                throw new InvalidOperationException("Invalid file type. Only JPG, JPEG, and PNG are allowed");

            const long maxFileSize = 3 * 1024 * 1024; // 3MB
            if (file.Length > maxFileSize)
                throw new InvalidOperationException("File size exceeds the maximum limit of 3 MB");

            var categoryPath = Path.Combine(_baseUploadPath, category);
            if (!Directory.Exists(categoryPath))
                Directory.CreateDirectory(categoryPath);

            var fileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(categoryPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            var request = _httpContextAccessor.HttpContext?.Request
                ?? throw new InvalidOperationException("HttpContextAccessor Not Valid!");

            var baseUrl = $"{request.Scheme}://{request.Host}";
            return $"{baseUrl}/Images/{category}/{fileName}";
        }

        public async Task DeleteFileAsync(string fileUrl)
        {
            if (string.IsNullOrEmpty(fileUrl))
                throw new ArgumentException("File path cannot be null or empty");

            var request = _httpContextAccessor.HttpContext?.Request
                ?? throw new InvalidOperationException("HttpContext is not available.");

            var baseUrl = $"{request.Scheme}://{request.Host}/";
            var relativePath = fileUrl.Replace(baseUrl, "", StringComparison.OrdinalIgnoreCase);

            var fullPath = Path.Combine(_webRootPath, relativePath.Replace("/", Path.DirectorySeparatorChar.ToString()));

            if (File.Exists(fullPath))
            {
                try
                {
                    await Task.Run(() => File.Delete(fullPath));
                }
                catch (Exception ex)
                {
                    throw new IOException($"Error occurred while deleting the file: {fullPath}");
                }
            }
        }
    }
}
