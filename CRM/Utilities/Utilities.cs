using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Utilities
{
    public static class Utilities
    {
        public static void RemoveFile(string root, string mainFolder, string subFolder, string file)
        {
            string fullPath = Path.Combine(root, mainFolder, subFolder, file);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

    }
}
