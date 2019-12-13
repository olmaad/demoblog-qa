using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DemoBlog.Tests
{
    class DataLoaderFactory
    {
        public string BaseDirectoryPath { get; set; }

        public DataLoader Create(string path)
        {
            return new DataLoader(Path.Combine(BaseDirectoryPath, path));
        }
    }
}
