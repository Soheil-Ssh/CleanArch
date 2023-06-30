using CleanArch.Common.Helpers;
using CleanArch.Common.Stores;
using Microsoft.AspNetCore.Http;

namespace CleanArch.Common.Extensions.HttpExtensions
{
    public static class FileExtensions
    {
        #region Save image

        public static async Task<string> SaveImageAsync(this IFormFile image,
            string path,
            string imageName = "",
            bool generateThumbnail = true)
        {
            if (imageName == StaticDataStore.NoPhotoImageName)
                return await Task.FromResult(StaticDataStore.NoPhotoImageName);

            if (string.IsNullOrEmpty(imageName))
                imageName = FileHelper.GenerateNewFileName(image.FileName);

            string savePath = FileHelper.GetImageSavePath(path, imageName);
            await FileHelper.SaveUploadedFileAsync(image, savePath);

            if (generateThumbnail)
            {
                string saveThumbPath = FileHelper.GetImageSavePath(path, imageName, true);
                await FileHelper.GenerateThumbImageAsync(savePath, saveThumbPath);
            }

            return await Task.FromResult(imageName);
        }

        #endregion

        #region Save file

        public static async Task<string> SaveFileAsync(this IFormFile file, string path, string fileName = "")
        {
            if (!string.IsNullOrEmpty(fileName))
                fileName = FileHelper.GenerateNewFileName(file.FileName);

            string savePath = FileHelper.GetSavePath(path, fileName);
            await FileHelper.SaveUploadedFileAsync(file, savePath);

            return await Task.FromResult(fileName);
        }

        #endregion
    }
}
