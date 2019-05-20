using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Extension
{
    public static class FileExtensions
    {
        public static bool IsImage(this IFormFile file)
        {
            return file.ContentType == "image/jpg" ||
                file.ContentType == "image/jpeg" ||
                file.ContentType == "image/png" ||
                file.ContentType == "image/gif";
        }

        public static bool IsCV(this IFormFile CVfile)
        {
            return CVfile.ContentType == "application/pdf" ||
             CVfile.ContentType == "application/doc" ||
             CVfile.ContentType == "application/docx" ||
             CVfile.ContentType == "application/pptx";

        }

        public async static Task<string> SaveAsync(this IFormFile file, string root, string mainFolder, string subFolder)
        {
            string fileName = Path.Combine(subFolder, Guid.NewGuid().ToString() + Path.GetFileName(file.FileName));

            string fullPath = Path.Combine(root, mainFolder, fileName);

            using (var stream = new FileStream(fullPath,FileMode.Create))
            {
               await file.CopyToAsync(stream);
            }
            return fileName;
        }

    }
}
