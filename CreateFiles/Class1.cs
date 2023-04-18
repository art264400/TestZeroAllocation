using System.Text;
using IOPath = System.IO.Path;

namespace CreateFiles
{
    public struct GuidFile
    {
        private const int SubfolderLength = 1;

        private const int TotalDigitsToFolders = 6;

        public string OriginalFileName { get; }

        public string Id { get; }

        public string Extension { get; }

        public GuidFile(string originalFileName) : this(originalFileName, Guid.NewGuid())
        {
        }

        public GuidFile(string originalFileName, Guid id)
        {
            OriginalFileName = originalFileName;
            Id = id.ToString("N");
            Extension = IOPath.GetExtension(originalFileName);
        }

        public string PhysicalPath
        {
            get
            {
                int groupCount = TotalDigitsToFolders / SubfolderLength;
                var sb = new StringBuilder(36 * 2, 36 * 2);
                for (int i = 0; i < groupCount; i++)
                {
                    sb.Append(Id.AsSpan(i, SubfolderLength)).Append("//");
                }
                return sb.Append(Id.AsSpan(TotalDigitsToFolders)).Append(Extension).ToString();
            }
        }

        public string LogicalPath => Id + Extension;

        /// <summary>
        /// </summary>
        /// <param name="path">Поле Link сущности Document</param>
        public static GuidFile FromPath(string path)
        {
            //  "N" - означает что должны быть только цифры
            var guidString = IOPath.GetFileNameWithoutExtension(path).Replace("-", "");
            Guid guid;
            if (!Guid.TryParseExact(guidString, "N", out guid))
            {
                throw new ArgumentException($"Значение {guidString} не является валидным гуидом", nameof(path));
            }
            return new GuidFile(path, guid);
        }
    }
}
