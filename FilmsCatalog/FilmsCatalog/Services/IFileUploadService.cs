using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace FilmsCatalog.Services
{
    public interface IFileUploadService
    {
        void DeleteFile(string path);
        Task<string> UploadImageAsync(IFormFile image);
    }
}