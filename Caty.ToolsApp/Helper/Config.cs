using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace Caty.ToolsApp.Helper
{
    internal class Config
    {
        public static string GetConfig(string key)
        {
            var builder = new ConfigurationBuilder().AddJsonFile("config.json"); //默认读取：当前运行目录
            var configuration = builder.Build();
            var configValue = configuration.GetSection(key).Value;
            return configValue;
        }

        public static void UpdateConfig<T>(string key, T value)
        {
            try
            {
                var filePath = Path.Combine(AppContext.BaseDirectory, "config.json");
                var json = File.ReadAllText(filePath);
                var valueJson = value.ToJson(new Json.OptionConfig());


                var node = JsonNode.Parse(json);

                //var jsonObj = JsonSerializer.Deserialize<dynamic>(json);
                var splittedKey = key.Split(":");
                var sectionPath = splittedKey[0];
                if (!string.IsNullOrEmpty(sectionPath) && splittedKey.Length > 1)
                {
                    var keyPath = splittedKey[1];
                    node[sectionPath][keyPath] = $@"{valueJson}";
                }
                else
                {
                    node[key] = $@"{valueJson}";
                }

                var writerOptions = new JsonWriterOptions
                {
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs)
                };
                using var fs = File.Create(filePath);
                using var writer = new Utf8JsonWriter(fs);
                var jsonObject = node.AsObject();
                var jsonSerializerOptions = new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                    WriteIndented = true,
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs)

                };
                jsonObject.WriteTo(writer);
                writer.Flush();
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }
    }
}
