using Honoured.Enumerations;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honoured.Utils
{
    public static class ImageUtils
    {
        private static readonly IDictionary<IconSize, string> DefaultIconStrings = new Dictionary<IconSize, string>();
        static ImageUtils()
        {
            LoadDefaultIconStrings();
        }

        private static void LoadDefaultIconStrings()
        {
            foreach ( var s in Enum.GetValues<IconSize>())
            {
                var filePath = Path.Combine(HonouredAppService.ProfileIconsFolderPath, $"Default_{s}.jpg");
                DefaultIconStrings.Add(s, GetBase64ForImage(filePath));
            }
        }

#pragma warning disable CA1416 // Validate platform compatibility
        public static Image GetScaledImage(Image toScale, IconSize size)
        {
            var scale = 32;
            switch (size)
            {
                case IconSize.small:
                    scale = 32;
                    break;
                case IconSize.medium:
                    scale = 64;
                    break;
                case IconSize.large:
                    scale = 128;
                    break;
                case IconSize.XLarge:
                    scale = 256;
                    break;
                default:
                    throw new ArgumentException($"Invalide size passed to GetScaledImage {size}",nameof(size));
            }
            return GetScaledImage(toScale, scale, scale);
        }
        public static Image GetScaledImage(Image toScale, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(toScale.HorizontalResolution, toScale.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(toScale, destRect, 0, 0, toScale.Width, toScale.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
        public static string GetBase64ForImage(string fileName)
        {
            var isFound = File.Exists(fileName);
            if (!isFound)
            {
                throw new FileNotFoundException("Could not locate the file!", fileName);
            }
            var bytes = File.ReadAllBytes(fileName);
            var b64 = Convert.ToBase64String(bytes);
            return b64;
        }

        public static string GetFilePath(long artistId, long artworkId)
        {
            //TODO fixme
            return Path.Combine(HonouredAppService.ImagesFolderPath, artistId.ToString());
        }

        public static string GetProfileIconPath(long artistId)
        {
            //TODO fixme
            return Path.Combine(HonouredAppService.ProfileIconsFolderPath, artistId.ToString());
        }

        public static string GenerateIconString(IconSize size, string filePath, string fileName)
        {
            var IconFolderPath = Path.Combine(filePath, GetIconFolderBySize(size));
            var IconFilePath = Path.Combine(IconFolderPath, fileName);
            if (!File.Exists(IconFilePath))
            {
                var originalFile = Path.Combine(filePath, fileName);
                if (!File.Exists(originalFile))
                {
                    throw new FileNotFoundException("Original image file not found!", originalFile);
                }
                var OriginalImage = Image.FromFile(originalFile);
                var resized = GetScaledImage(OriginalImage, size);
                Directory.CreateDirectory(IconFolderPath);
                resized.Save(IconFilePath);
            }
            return GetBase64ForImage(IconFilePath);
        }

        public static string SaveArtistIconAndGetSmallString(IconSize size, string filePath, long artistId, string extension)
        {
            var toRet = string.Empty;

            foreach (var iconSize in Enum.GetValues<IconSize>())
            {
                var fileName = Path.Combine(filePath, GetIconNamebySize(artistId,size,extension));
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                var originalFile = Path.Combine(filePath, fileName);
                if (!File.Exists(originalFile))
                {
                    throw new FileNotFoundException("Original image file not found!", originalFile);
                }
                var OriginalImage = Image.FromFile(originalFile);
                var resized = GetScaledImage(OriginalImage, size);

                resized.Save(fileName);
                if (iconSize== IconSize.small)
                {
                    toRet = GetBase64ForImage(fileName);
                }
            }
            return toRet;
        }

        private static string GetIconNamebySize(long artistId, IconSize size, string extension)
        {
            return $"{artistId}_{size}{extension}";
        }

        public static string GetProfileIcon(long id, int size, string extension)
        {
            IconSize sizeToGet = IconSize.small;
            try
            {
                sizeToGet = (IconSize) size;
            }
            catch (Exception)
            {
                //do notthing
            }
            var iconPath = GetProfileIconPath(id);
            var iconFileName = Path.Combine(iconPath, GetIconNamebySize(id, sizeToGet,extension));
            if (!File.Exists(iconFileName))
            {
                //TODO try to locate other size files and get our size from them
                return DefaultIconStrings[sizeToGet];
            }
            return GetBase64ForImage(iconFileName);
        }

        private static string GetIconFolderBySize(IconSize size)
        {
            return size.ToString();
        }
    }

#pragma warning restore CA1416 // Validate platform compatibility
}
