using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Text.Json;
using System.Text.Encodings.Web;
using PrintSiteBuilder.Models;
using PrintSiteBuilder.Models.Print;
using PrintSiteBuilder.Print;

namespace PrintSiteBuilder.Utilities
{
    public class Json
    {
        public void SerializeItemsConfig(ItemsConfig itemsConfig)
        {
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true // 読みやすい形式で出力
            };
            string jsonString = JsonSerializer.Serialize(itemsConfig, options);
            File.WriteAllText(GlobalConfig.ItemsConfigPath, jsonString);
        }
        public void SerializeDocsConfig(DocsConfig itemsConfig)
        {
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true // 読みやすい形式で出力
            };
            string jsonString = JsonSerializer.Serialize(itemsConfig, options);
            File.WriteAllText(GlobalConfig.DocsConfigPath, jsonString);
        }
        public void SerializePrintConfig(IPrint iPrint)
        {
            var printClass = iPrint.GetPrintConfigs();
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true // 読みやすい形式で出力
            };
            string jsonString = JsonSerializer.Serialize(printClass, options);
            string ExportPath = Path.Combine(@"C:\drive\work\www\item\print\100000", $"{iPrint.PresentationId}.json");
            File.WriteAllText(ExportPath, jsonString);
        }
        public void SerializeKeysConfig(KeysConfig keysConfig)
        {
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true // 読みやすい形式で出力
            };
            string jsonString = JsonSerializer.Serialize(keysConfig, options);
            File.WriteAllText(GlobalConfig.KeysConfigPath, jsonString);
        }
        public ItemsConfig DeserializeItemsConfig()
        {
            string jsonString = File.ReadAllText(GlobalConfig.ItemsConfigPath);
            return JsonSerializer.Deserialize<ItemsConfig>(jsonString);
        }
        public DocsConfig DeserializeDocsConfig()
        {
            string jsonString = File.ReadAllText(GlobalConfig.DocsConfigPath);
            return JsonSerializer.Deserialize<DocsConfig>(jsonString);
        }
        public ItemsConfig DeserializeKeysConfig()
        {
            string jsonString = File.ReadAllText(GlobalConfig.KeysConfigPath);
            return JsonSerializer.Deserialize<ItemsConfig>(jsonString);
        }
        public List<PrintConfig> DeserializePrintConfig(IPrint printConfig)
        {
            string path = Path.Combine(@"C:\drive\work\www\item\print\100000", $"{printConfig.PresentationId}.json");
            string jsonString = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<PrintConfig>>(jsonString);
        }
    }
}
