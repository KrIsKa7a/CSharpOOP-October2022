namespace Logger.Core.Utilities
{
    public static class FileValidator
    {
        public static bool PathExists(string path)
        {
            return Directory.Exists(path);
        }
    }
}
