namespace CleanArch.Common.Extensions.StringExtensions
{
    public static class ImagePathExtensions
    {
        #region To image src

        public static string ToImageSrc(this string path, string imageName)
            => path.Replace("wwwroot", String.Empty) + imageName;

        #endregion

        #region To image thumb src

        public static string ToImageThumbSrc(this string path, string imageName, string thumbDirectory = "thumb/")
            => path.Replace("wwwroot", String.Empty) + thumbDirectory + imageName;

        #endregion
    }
}
