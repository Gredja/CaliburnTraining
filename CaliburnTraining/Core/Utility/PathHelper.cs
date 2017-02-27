using System.IO;
using System.Reflection;

namespace Core.Utility
{
  public  static class PathHelper
    {
        static string rootPath;
        static PathHelper()
        {
            rootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        public static string MakeAbsPath(string relPath)
        {
            if (relPath == null) return null;
            string path = relPath.TrimStart('/', '\\');
            return Path.Combine(rootPath, path);
        }
    }
}
