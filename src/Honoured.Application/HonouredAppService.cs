using System;
using System.Collections.Generic;
using System.Text;
using Honoured.Localization;
using Volo.Abp.Application.Services;

namespace Honoured
{
    /* Inherit your application services from this class.
     */
    public abstract class HonouredAppService : ApplicationService
    {
        public static string ImagesFolderPath { get; set; } = @"D:\test\hon";

        public static long MaxFileSize { get; set; } = 1000000000;

        public static string[] AllowedExtensions { get; set; } = new string[] {".jpg",".png",".jpeg",".bmp",".tif",".svg" };
        protected HonouredAppService()
        {
            LocalizationResource = typeof(HonouredResource);
        }
        //TODO
        public static int SchedulGenerationInterval { get; internal set; }
        public static string ProfileIconsFolderPath { get; internal set; } = @"D:\test\hon\Profiles";
    }
}
