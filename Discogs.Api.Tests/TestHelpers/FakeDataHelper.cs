using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Discogs.Api.Tests.TestHelpers
{
    public class FakeDataHelper
    {
        public static T DeserializeFromJsonFile<T>(string filename)
        {
            var assembly = typeof(FakeDataHelper).GetTypeInfo().Assembly;
            var assemblyName = assembly.GetName().Name;
            var stream = assembly.GetManifestResourceStream($"{assemblyName}.TestData.{filename}.json");
            var text = string.Empty;

            using (StreamReader reader = new(stream, Encoding.UTF8))
            {
                text = reader.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<T>(text);
        }
    }
}
