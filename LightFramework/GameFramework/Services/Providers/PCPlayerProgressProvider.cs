using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;

namespace GameFramework.Services.Providers
{
    public class PCPlayerProgressProvider : IPlayerProgressProvider
    {
        private readonly string _folder;

        public PCPlayerProgressProvider()
        {
            string folderName = Assembly.GetEntryAssembly().GetName().Name;
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            _folder = Path.Combine(appData, folderName);
            if (!Directory.Exists(_folder)) Directory.CreateDirectory(_folder);
        }

        public void PutSaveData(string filename, object saveData)
        {
            string path = Path.Combine(_folder, filename);
            if (File.Exists(path)) File.Delete(path);
            File.WriteAllText(path, JsonConvert.SerializeObject(saveData));
        }

        public T GetSaveData<T>(string filename) where T : class, new()
        {
            string path = Path.Combine(_folder, filename);

            if (!File.Exists(path)) return default(T);

            try
            {
                string json = File.ReadAllText(path);
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch
            {
                return default(T);
            }
        }
    }
}
