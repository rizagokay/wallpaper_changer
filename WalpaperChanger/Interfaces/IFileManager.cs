using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalpaperChanger.JsonObjects;

namespace WalpaperChanger.Interfaces
{
    public interface IFileManager
    {
        void CreateStorageFileIfNotExists();
        JsonProgramData GetProgramData();
        void SaveJsonData(JsonProgramData data);
    }
}
