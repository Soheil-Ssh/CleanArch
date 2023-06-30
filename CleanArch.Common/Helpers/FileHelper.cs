using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace CleanArch.Common.Helpers
{
    public static class FileHelper
    {
        #region Get Save Path

        public static string GetSavePath(string basePath, string fileName)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), basePath, fileName);
        }

        public static string GetImageSavePath(string basePath, string fileName, bool isExistThumbnail = false)
        {
            if (isExistThumbnail)
                basePath += "thumb/";

            return Path.Combine(Directory.GetCurrentDirectory(), basePath, fileName);
        }

        #endregion

        #region generate new file name

        public static string GenerateNewFileName(string fileName)
        {
            string newName = Guid.NewGuid().ToString();
            string fileExtension = Path.GetExtension(fileName);
            return newName + fileExtension;
        }

        #endregion

        #region Save Uploaded File

        public static async Task SaveUploadedFileAsync(IFormFile file, string savePath)
        {
            await using FileStream fileStream = new FileStream(savePath, FileMode.Create);
            await file.CopyToAsync(fileStream);
        }

        #endregion

        #region Generate Thumb Image

        public static async Task GenerateThumbImageAsync(string inputPath,
            string outputPath,
            int minSize = 200)
        {
            using Image image = await Image.LoadAsync(inputPath);
            float height;
            float width;
            float ratio;

            if (image.Width > image.Height)
            {
                ratio = (float)image.Width / (float)image.Height;
                height = minSize;
                width = height * ratio;
            }
            else
            {
                ratio = (float)image.Height / (float)image.Width;
                width = minSize;
                height = width * ratio;
            }

            image.Mutate(x =>
                x.Resize((int)width, (int)height));

            await image.SaveAsync(outputPath);
        }

        #endregion

        #region Delete File

        public static void DeleteFile(string path)
        {
            if (File.Exists(path))
                File.Delete(path);
            else
                throw new FileNotFoundException($"File not found from path = ${path}");
        }

        #endregion

        #region Delete Image

        public static void DeleteImage(string imageName, string path, bool isExistThumbnail = false)
        {
            DeleteFile(GetImageSavePath(path, imageName));
            if (isExistThumbnail)
                DeleteFile(GetImageSavePath(path, imageName, true));
        }

        #endregion
    }
}
