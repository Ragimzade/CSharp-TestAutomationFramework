using System;
using System.IO;

namespace Framework.Utils
{
    public static class FileUtils
    {
        private const string ScreenShotsFolder = "ScreenShots";

        public static string BuildDirectoryPath()
        {
            var location = Path.Combine(AppContext.BaseDirectory, ScreenShotsFolder);
            if (!Directory.Exists(location))
            {
                Directory.CreateDirectory(location);
            }
            return location;
        }
        public static void CleanDirectory(string directoryLocation)
        {
            foreach (var file in new DirectoryInfo(directoryLocation).GetFiles())
            {
                file.Delete();
            }
        }
    }
}