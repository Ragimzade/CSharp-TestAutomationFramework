﻿using System;
using System.IO;

namespace Test.utils
{
    public static class FileUtils
    {
        private const string ScreenShotsFolder = "ScreenShots";

        public static string GetOutputDirectory()
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