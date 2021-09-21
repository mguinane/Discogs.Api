using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using System.Text;

namespace Discogs.Api.Tests.TestHelpers
{
    public class FakeDataHelper
    {
        public static T DeserializeFromJsonFile<T>(string filename)
        {
            return JsonConvert.DeserializeObject<T>(ReadFromJsonFile(filename));
        }

        public static string ReadFromJsonFile(string filename)
        {
            var assembly = typeof(FakeDataHelper).GetTypeInfo().Assembly;
            var assemblyName = assembly.GetName().Name;
            var stream = assembly.GetManifestResourceStream($"{assemblyName}.TestData.{filename}.json");
            var text = string.Empty;

            using (StreamReader reader = new(stream, Encoding.UTF8))
            {
                text = reader.ReadToEnd();
            }

            return text;
        }
    }
}
