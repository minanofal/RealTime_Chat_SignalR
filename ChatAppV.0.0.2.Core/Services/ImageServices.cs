using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppV._0._0._2.Core.Services
{
    public static class ImageServices
    {
        public static readonly List<string> ImageExtintions = new List<string> { ".png", ".jpg", ".png" };
        public static readonly long MaxLength = 1048576;
        public static readonly string IntialImageMale = @"Resources\Images\2c257337-87c8-47e6-8f4a-4d36716368a3.png";
        public static readonly string IntialImageFemale = @"Resources\Images\edbcea89-5d18-419f-9f38-5db6795bdd60.png";
        public static string CheckImageValidation(IFormFile image)
        {
            string message = string.Empty;
            if (!ImageExtintions.Contains(Path.GetExtension(image.FileName).ToLower()))
            {
                message += "Image must have one extention of";
                foreach (string ext in ImageExtintions)
                {
                    message += ext + " ";
                }
            }
            if (image.Length > MaxLength)
            {
                message += $"Max size for image is {MaxLength / 1048576 } MG ";
            }

            return message;
        }

        public static string UploadImage(IFormFile image)
        {

            var folderName = Path.Combine("Resources", "Images");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            var fullPath = Path.Combine(pathToSave,fileName);
            var dbPath = Path.Combine(folderName, fileName);
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                image.CopyTo(stream);
            }
            return dbPath;
        }

        public static bool DeleteImage(string path)
        {
            if (path != @"Resources\Images\2c257337-87c8-47e6-8f4a-4d36716368a3.png" && path != @"Resources\Images\edbcea89-5d18-419f-9f38-5db6795bdd60.png")
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
                return true;

            }

    }
}
