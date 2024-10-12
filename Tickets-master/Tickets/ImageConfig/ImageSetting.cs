namespace Tickets.ImageConfig
{
    public static class ImageSetting
    {
        public static string UploadImage(IFormFile formFile)
        {
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Events");
            var guid = Guid.NewGuid().ToString();
            var filename = guid + $"{Path.GetFileName(formFile.FileName)}";

            var filepath = Path.Combine(folderPath, filename);

            using var filestreem = new FileStream(filepath, FileMode.Create);
            formFile.CopyTo(filestreem);
            return filename;
        }
    }
}
