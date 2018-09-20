using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalpaperChanger.Interfaces;
using WalpaperChanger.JsonObjects;

namespace WalpaperChanger.Objects
{
    public class FileManager : IFileManager
    {

        private string commonAppData;
        private string jsonPath;

        public FileManager()
        {
            commonAppData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            jsonPath = Path.Combine(commonAppData, "WPChanger\\jsondata.json");
        }

        public void CreateStorageFileIfNotExists()
        {
            if (!Directory.Exists(Path.GetDirectoryName(jsonPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(jsonPath));
            }

            if (!File.Exists(jsonPath))
            {
                var jsonData = JsonConvert.SerializeObject(new JsonProgramData { LastSelectedCategory = "" });

                File.WriteAllText(jsonPath, jsonData);
            }
        }

        public JsonProgramData GetProgramData()
        {
            CreateStorageFileIfNotExists();

            var redJson = File.ReadAllText(jsonPath);

            var jsonData = JsonConvert.DeserializeObject<JsonProgramData>(redJson);

            return jsonData;
        }

        public void SaveJsonData(JsonProgramData data)
        {
            var jsonData = JsonConvert.SerializeObject(data);

            File.WriteAllText(jsonPath, jsonData);
        }
    }
}
