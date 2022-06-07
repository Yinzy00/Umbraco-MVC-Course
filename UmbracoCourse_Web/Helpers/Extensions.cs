using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UmbracoCourse_Web.Helpers
{
    public static class Extensions
    {
        public static bool HasFiles(this IEnumerable<IFormFile> files)
        {
            var first = files != null ? files.FirstOrDefault() : default(IFormFile);
            return first != null && first.Length > 0;
        }

        public static bool ContainsImages(this IEnumerable<IFormFile> files)
        {
            return files.Any(file => file.IsImage());
        }

        public static bool IsImage(this IFormFile file)
        {
            if (file.ContentType.Contains("image"))
            {
                return true;
            }

            var formats = new[] { ".jpg", ".png", ".gif", ".jpeg" };
            return formats.Any(item => file.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase));
        }
    }
}
