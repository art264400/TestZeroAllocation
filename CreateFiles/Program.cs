using CreateFiles;

string[] fileExtensions = new[]
       {
            "bmp", "png", "jpg", "jpeg", "gif", "tif", "tiff", "doc", "docx", "rtf", "txt", "pdf", "xls", "xlsx", "rar", "zip", "7z", "ppt", "pptx", "odf"
        };
var rnd = new Random();
int count = 0;

while (true)
{

    var ext = GetRandomFileExtension(rnd, fileExtensions);
    var guidFile = new GuidFile($"ad.{ext}");
    string absolutePath = Path.Combine("C:\\Users\\Arthur\\Documents\\Files", guidFile.PhysicalPath);
    var directoryName = Path.GetDirectoryName(absolutePath);
    Directory.CreateDirectory(directoryName);
    File.WriteAllBytes(absolutePath, new byte[rnd.Next(100, 200)]);
    count++;
}

string GetRandomFileExtension(Random random, string[] fileExtensions)
{
    int randomIndex = random.Next(fileExtensions.Length);
    return fileExtensions[randomIndex];
}