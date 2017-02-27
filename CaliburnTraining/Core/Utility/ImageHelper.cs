using System.IO;

namespace Core.Utility
{
    public static class ImageHelper
    {
        public static string FormattingImagePathForUserPhoto(string imagePath)
        {
            string fileName = !string.IsNullOrEmpty(imagePath) ? imagePath : "noPhoto.png";
            return $"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName}\\Media\\{fileName}";
        }
    }
}
