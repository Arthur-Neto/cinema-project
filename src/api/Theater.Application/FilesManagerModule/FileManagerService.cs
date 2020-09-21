using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Theater.Application.FilesManagerModule
{
    public interface IFileManagerService
    {
        Task<string> CreateCoverImageAsync(IFormFile file, int movieId);
        void RemoveCoverImage(int movieId);
    }

    public class FileManagerService : IFileManagerService
    {
        private readonly string STORED_IMG_FOLDER = $"{Environment.CurrentDirectory}\\wwwroot\\movies-imgs\\";

        public async Task<string> CreateCoverImageAsync(IFormFile file, int movieId)
        {
            try
            {
                if (file.Length > 0)
                {
                    var imgFilename = $"{movieId}{Path.GetExtension(file.FileName)}";
                    var fullFilePath = Path.Combine(STORED_IMG_FOLDER, imgFilename);

                    if (!Directory.Exists(STORED_IMG_FOLDER))
                    {
                        Directory.CreateDirectory(STORED_IMG_FOLDER);
                    }

                    using var stream = new FileStream(fullFilePath, FileMode.Create);
                    await file.CopyToAsync(stream);

                    return fullFilePath;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RemoveCoverImage(int movieId)
        {
            try
            {
                if (Directory.GetFiles(STORED_IMG_FOLDER, $"{movieId}.*").Length > 0)
                {
                    var di = new DirectoryInfo(STORED_IMG_FOLDER);

                    foreach (var file in di.GetFiles($"{movieId}.*"))
                    {
                        file.Delete();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
