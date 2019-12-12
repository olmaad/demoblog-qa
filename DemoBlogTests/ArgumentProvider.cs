using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TestDataLib;

namespace DemoBlogTests
{
    class ArgumentProvider
    {
        static object[] LoadData(string path)
        {
            var data = new Data();

            using (StreamReader reader = new StreamReader(path))
            {
                var dataString = reader.ReadToEnd();

                data = JsonConvert.DeserializeObject<Data>(dataString);
            }

            data.Name = path;

            return new object[] { data };
        }
    }
}
