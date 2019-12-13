using Newtonsoft.Json;
using System.IO;
using DemoBlog.TestDataLib;

namespace DemoBlog.Tests
{
    class DataLoader
    {
        private string mPath;
        private Data mData;

        public Data Data
        {
            get
            {
                if (mData == null)
                {
                    Load();
                }

                return mData;
            }
        }

        public DataLoader(string path)
        {
            mPath = path;
        }

        private void Load()
        {
            mData = new Data();

            using (StreamReader reader = new StreamReader(mPath))
            {
                var dataString = reader.ReadToEnd();

                mData = JsonConvert.DeserializeObject<Data>(dataString);
            }

            mData.Name = mPath;
        }
    }
}
