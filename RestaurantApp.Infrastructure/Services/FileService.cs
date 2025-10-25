using Microsoft.AspNetCore.Http;
using RestaurantApp.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RestaurantApp.Infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly IAppEnvirnment _env;

        public FileService(IAppEnvirnment env)
        {
           _env = env;
        }

        public Task<bool> DeleteFile(string? filename, string FolderName)
        {
            if (filename != null)
            {
                var FilePath = Path.Combine(_env.WebPathRoot, "Files", FolderName, filename);
                if (File.Exists(FilePath))
                {
                    File.Delete(FilePath);
                    return Task.FromResult(true);

                }
            }
            return Task.FromResult(false);
        
        }

        public async Task<string> UploadFile(IFormFile file, string folderName)
        {
         
            string FolderPath = Path.Combine(_env.WebPathRoot, "Files", folderName);
            string FileName = $"{Guid.NewGuid()} {file.FileName}";
            string FilePath = Path.Combine(FolderPath, FileName);
            using var filestream = new FileStream(FilePath, FileMode.Create);
            await file.CopyToAsync(filestream);
            return FileName;
        }

   

    }
}
