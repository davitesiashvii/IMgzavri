using File = IMgzavri.FileStore.Domain;

namespace IMgzavri.FileStore.Infrastructure.Helpers
{
    public static class FileHelper
    {
        public static bool Exists(string path) => System.IO.File.Exists(path);

        public static string BuildPath(IMgzavri.FileStore.Domain.File file, string basePath, string mainFolder)
        {
            string path = Path.Combine(basePath + $"\\{mainFolder}\\");

            path = EnsureDirectoryCreation(path, file.CreateDate);

            return Path.Combine(path, Path.GetFileNameWithoutExtension(file.Name) + file.Extension);
        }

        public static string BuildPathForFileServer(IMgzavri.FileStore.Domain.File file, string requestPath, string apiUrl)
        {
            var day = file.CreateDate.Day.ToString();
            var month = file.CreateDate.Month.ToString();
            var year = file.CreateDate.Year.ToString();

            string path = string.Join('/', apiUrl + requestPath, year, month, day, file.Name);

            return path;
        }

        private static string EnsureDirectoryCreation(string path, DateTimeOffset creationDate)
        {
            var day = creationDate.Day.ToString();
            var month = creationDate.Month.ToString();
            var year = creationDate.Year.ToString();

            var combined = Path.Combine(path, $"{year}\\", $"{month}\\", $"{day}\\");

            if (!Directory.Exists(combined))
                Directory.CreateDirectory(combined);

            return combined;
        }
    }
}
