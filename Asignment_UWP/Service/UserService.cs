using Asignment_UWP.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Credentials;

namespace Asignment_UWP.Service
{
    class UserService
    {
        private string apiBaseUrl = "https://music-i-like.herokuapp.com/api";
        private PasswordVault store = new PasswordVault();
        public async Task<Credential> Login(Account account)
        {
            try
            {
                var accountJson = JsonConvert.SerializeObject(account);
                HttpClient httpClient = new HttpClient();
                Console.WriteLine(accountJson);
                var httpContent = new StringContent(accountJson, Encoding.UTF8, "application/json");
                var requestConnection = await httpClient.PostAsync($"{apiBaseUrl}/v1/accounts/authentication", httpContent);
                Console.WriteLine(requestConnection.StatusCode);                                                                                     // chờ phản hồi, lấy kết quả
                if (requestConnection.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = await requestConnection.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Credential>(content);
                    store.Add(new PasswordCredential("token", "hinhnx", result.access_token));
                    return result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }
        public async Task<bool> Register(User user)
        {
            var userJson = JsonConvert.SerializeObject(user);
            Debug.WriteLine(userJson);
            var http = new HttpClient();
            var httpContent = new StringContent(userJson, Encoding.UTF8, "application/json");
            var request = await http.PostAsync($"{apiBaseUrl}/v1/accounts", httpContent);
            Debug.WriteLine(request);
            if (request.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<User> GetProfile()
        {
            PasswordCredential credential = store.Retrieve("token", "hinhnx");
            Debug.WriteLine(credential.Password);
            var result = credential.Password;
            if (result == null)
            {
                return null;
            }
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {result}");
                var requestConnection =
                    await httpClient.GetAsync($"{apiBaseUrl}/v1/accounts");
                Console.WriteLine(requestConnection.StatusCode);
                if (requestConnection.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = await requestConnection.Content.ReadAsStringAsync();
                    Debug.WriteLine("Getting information");
                    Debug.WriteLine(content);
                    var account = JsonConvert.DeserializeObject<User>(content);
                    Console.WriteLine(content);
                    return account;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }
    }
}
