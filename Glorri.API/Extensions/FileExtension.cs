namespace Glorri.API.Extensions
{
    public static class FileExtension
    {
        public static string UploadFile(this IFormFile file, string root, string path)
        {
            var filename = Guid.NewGuid().ToString() + file.FileName;
            var fullPath = Path.Combine(root, path, filename);
            using (FileStream fs = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(fs);
            }
            return filename;
        }
    }
}

