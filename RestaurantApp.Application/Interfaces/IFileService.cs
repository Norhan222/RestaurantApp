using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;


namespace RestaurantApp.Application.Interfaces
{
    //Application shoud depend on abstraction,not on technical details
    public interface IFileService
    {
        Task<string> UploadFile(IFormFile file, string folderName);
        Task<bool> DeleteFile(string? file, string FolderName);
    }
}
