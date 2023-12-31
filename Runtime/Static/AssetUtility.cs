using System.IO;
namespace Utilities
{
    public static class AssetUtility
    {
        public static string GetUniquePath(string path, string extension)
        {
            int index = 1;

            string uniquePathWithoutExtension = path;

            while (FileExists(uniquePathWithoutExtension, extension))
            {
                uniquePathWithoutExtension = path + "_" + index;
                index++;
            }
            return uniquePathWithoutExtension + "." + extension;
        }
        public static bool FileExists(string path, string extension)
        {
            return new FileInfo(path + "." + extension).Exists;
        }
    }
}
