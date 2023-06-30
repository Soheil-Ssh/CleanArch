using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;

namespace CleanArch.Common.Extensions.HttpExtensions
{
    public static class SecurityExtensions
    {
        #region Validation image

        public const int ImageMinimumBytes = 512;

        public static bool IsValidImage(this IFormFile file)
        {
            if (file.ContentType.ToLower() != "image/jpg" &&
                file.ContentType.ToLower() != "image/jpeg" &&
                file.ContentType.ToLower() != "image/pjpeg" &&
                file.ContentType.ToLower() != "image/x-png" &&
                file.ContentType.ToLower() != "image/png")
            {
                return false;
            }

            if (Path.GetExtension(file.FileName).ToLower() != ".jpg"
                && Path.GetExtension(file.FileName).ToLower() != ".png"
                && Path.GetExtension(file.FileName).ToLower() != ".jpeg")
                return false;

            try
            {
                if (!file.OpenReadStream().CanRead)
                    return false;

                if (file.Length < ImageMinimumBytes)
                    return false;

                byte[] buffer = new byte[ImageMinimumBytes];
                file.OpenReadStream().Read(buffer, 0, ImageMinimumBytes);
                string content = Encoding.UTF8.GetString(buffer);
                if (Regex.IsMatch(content, @"<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext|<cross\-domain\-policy",
                        RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
                    return false;
            }
            catch (Exception)
            {
                return false;
            }

            try
            {
                using (var bitmap = new Bitmap(file.OpenReadStream()))
                { }
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                file.OpenReadStream().Position = 0;
            }

            return true;
        }

        #endregion

        #region Validation any files

        public static bool IsValidFile(this IFormFile file)
        {
            string extension = GetFileExtension(file);

            switch (extension.ToUpper())
            {
                case ("ACTION"):
                case ("APK"):
                case ("APP"):
                case ("BAT"):
                case ("BIN"):
                case ("CMD"):
                case ("COM"):
                case ("COMMAND"):
                case ("CPL"):
                case ("CSH"):
                case ("EXE"):
                case ("GADGET"):
                case ("INF1"):
                case ("INS"):
                case ("INX"):
                case ("IPA"):
                case ("ISU"):
                case ("JOB"):
                case ("JSE"):
                case ("KSH"):
                case ("LNK"):
                case ("MSC"):
                case ("MSI"):
                case ("MSP"):
                case ("MST"):
                case ("OSX"):
                case ("OUT"):
                case ("PAF"):
                case ("PIF"):
                case ("PRG"):
                case ("PS1"):
                case ("REG"):
                case ("RGS"):
                case ("RUN"):
                case ("SCR"):
                case ("SCT"):
                case ("SHB"):
                case ("SHS"):
                case ("U3P"):
                case ("VB"):
                case ("VBE"):
                case ("VBS"):
                case ("VBSCRIPT"):
                case ("WORKFLOW"):
                case ("WS"):
                case ("WSF"):
                case ("WSH"):
                    {
                        return false;
                    }
            }

            return true;
        }

        #endregion

        #region Get file extension

        private static string GetFileExtension(IFormFile file)
        {
            string extension = Path.GetExtension(file.FileName);
            return extension.Replace(".", string.Empty);
        }

        #endregion
    }
}
