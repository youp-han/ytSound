using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace ytSound.utility
{
    internal static class StorageUtility
    {
        private static readonly string storageFilePath = Path.Combine(
                Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData), "ytSoundCheckedUrls.json");

        public static void SaveCheckedUrls(Dictionary<string, string> checkedUrls)
        {
            Dictionary<string, string> existingUrls = LoadCheckedUrls();

            foreach (var url in checkedUrls)
            {
                existingUrls[url.Key] = url.Value;
            }

            string jsonString = JsonSerializer.Serialize(existingUrls);
            MessageBox.Show(":)" + storageFilePath);
            File.WriteAllText(storageFilePath, jsonString);
        }

        public static Dictionary<string, string> LoadCheckedUrls()
        {
            if (File.Exists(storageFilePath))
            {
                string jsonString = File.ReadAllText(storageFilePath);
                return JsonSerializer.Deserialize
                    <Dictionary<string, string>>(jsonString) ?? new Dictionary<string, string>();
            }

            return new Dictionary<string, string>();
        }

        public static void DeleteCheckedUrls(Dictionary<string, string> checkedUrls)
        {
            Dictionary<string, string> existingUrls = LoadCheckedUrls();

            foreach (var url in checkedUrls.Keys)
            {
                if (existingUrls.ContainsKey(url))
                {
                    existingUrls.Remove(url);
                }
            }

            string jsonString = JsonSerializer.Serialize(existingUrls);
            File.WriteAllText(storageFilePath, jsonString);
        }
    }
}
