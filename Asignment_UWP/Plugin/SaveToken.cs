using Asignment_UWP.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Asignment_UWP.Plugin
{
    class SaveToken
    {
        public async Task<Credential> LoadAccessTokenFromFile()
        {
            try
            {
                StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                StorageFile storageFile = await storageFolder.GetFileAsync("token.txt");
                var fileContent = await FileIO.ReadTextAsync(storageFile);
                var credential = JsonConvert.DeserializeObject<Credential>(fileContent);
                return credential;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }
        public async Task WriteTokenToFile(string content)
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile storageFile = await storageFolder.CreateFileAsync("token.txt", CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(storageFile, content);
        }
    }
}
