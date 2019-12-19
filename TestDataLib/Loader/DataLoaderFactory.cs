using System.IO;

namespace DemoBlog.TestDataLib.Loader
{
    public class DataLoaderFactory
    {
        public string BaseDirectoryPath { get; set; }

        public DataLoader Create(string path)
        {
            return new DataLoader(Path.Combine(BaseDirectoryPath, path));
        }
    }
}
