string inputPath = "\\opt\\Files\\1\\0\\0\\0\\e3f1f447288cd3dddcd9c0d4f2.req";
string result = TransformPath(inputPath, "\\opt\\Files\\");
Console.WriteLine(result);

static string TransformPath(string path, string rootFolder)
{
    string relativePath = path.Replace(rootFolder, "").TrimStart(Path.DirectorySeparatorChar);
    return string.Concat(relativePath.Split(Path.DirectorySeparatorChar));
}