using Asignment_UWP.Entity;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Security.Credentials;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Text;

namespace Asignment_UWP.Service
{
    class SongService
    {
        private string apiBaseUrl = "https://music-i-like.herokuapp.com/api";
        private PasswordVault store = new PasswordVault();

        public async Task<bool> CreateSong(Song song)
        {
            PasswordCredential credential = store.Retrieve("token", "hinhnx");
            var token = credential.Password;
            var jsonString = JsonConvert.SerializeObject(song);
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            HttpContent contentToSend = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var result = await httpClient.PostAsync($"{apiBaseUrl}/v1/songs", contentToSend);
            if (result.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Song>> GetLatestSong()
        {
            List<Song> result = new List<Song>();
            try
            {
                HttpClient httpClient = new HttpClient();
                var requestConnection = await httpClient.GetAsync($"{apiBaseUrl}/v1/songs");
                Debug.WriteLine(requestConnection);
                if (requestConnection.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = await requestConnection.Content.ReadAsStringAsync();
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Song>>(content);
                    return result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return result;
        }

        public async Task<List<Song>> GetMySong()
        {
            List<Song> result = new List<Song>();
            try
            {
                PasswordCredential credential = store.Retrieve("token", "hinhnx");
                var token = credential.Password;
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                var requestConnection = await httpClient.GetAsync($"{apiBaseUrl}/v1/songs/mine");
                Debug.WriteLine(requestConnection);
                if (requestConnection.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = await requestConnection.Content.ReadAsStringAsync();
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Song>>(content);
                    return result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return result;
        }

    }
}
