using Newtonsoft.Json;
using SmartReport.Models.Entities;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SmartReport.DataService
{
    static class AccountDataService
    {
        private static string baseUrl = "https://smart-report-backend.azurewebsites.net/api/Account";
        //private static HttpClient httpClient;

        static AccountDataService()
        {
           // httpClient = new HttpClient();
          //  httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }


        public static async Task<bool> LoginAsync(LoginModel model)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                httpClient.Timeout = new TimeSpan(hours: 0, minutes: 10, seconds: 0);
                var content = new StringContent(JsonConvert.SerializeObject(model), System.Text.Encoding.UTF8);
                var url = baseUrl + "/login";

                var mValue = new MediaTypeHeaderValue("application/json");
                content.Headers.ContentType = mValue;
                var response = await httpClient.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(result);
                    StaticHandler.Token = tokenResponse.AccessToken;
                    StaticHandler.Email = model.Email;
                    return true;
                }
                else
                {
                    StaticHandler.Token = "";
                    return false;
                }
            }
            catch (Exception ex)
            {
                StaticHandler.Token = "";
                return false;
            }
        }

        public static async Task RegisterAsync(RegisterModel model)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                httpClient.Timeout = new TimeSpan(hours: 0, minutes: 10, seconds: 0);
                var content = new StringContent(JsonConvert.SerializeObject(model), System.Text.Encoding.UTF8);
                var url = baseUrl + "/register";

                var mValue = new MediaTypeHeaderValue("application/json");
                content.Headers.ContentType = mValue;
                var response = await httpClient.PostAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();
                await LoginAsync(new LoginModel() { Email = model.Email, Password = model.Password });
            }
            catch (Exception ex)
            {
                StaticHandler.Token = "";
            }
        }
    }
}
