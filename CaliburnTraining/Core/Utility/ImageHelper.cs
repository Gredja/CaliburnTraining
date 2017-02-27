namespace Core.Utility
{
    public static class ImageHelper
    {
        public static string FormattingImagePathForUserPhoto(string imagePath)
        {
            string path = !string.IsNullOrEmpty(imagePath) ? imagePath : "noPhoto.png";
            return $"pack://application:,,,/CaliburnTraining;component/Media/{path}";
        }
    }
}
