using Newtonsoft.Json;
using SmartReport.Models.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SmartReport.DataService
{
    static class TaskDataService
    {
        private static string baseUrl = "https://smart-report-backend.azurewebsites.net/api";

        public static async Task<List<TaskModel>> GetMissedTasksAsync()
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {StaticHandler.Token ?? ""}");
                httpClient.Timeout = new TimeSpan(hours: 0, minutes: 10, seconds: 0);
                var url = baseUrl + "/Task/GetMissedTasks";
                var response = await httpClient.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                var tasks = JsonConvert.DeserializeObject<List<TaskModel>>(result);
                return tasks;
            }
            catch (Exception ex)
            {
                return new List<TaskModel>();
            }
        }

        public static async Task CreateReport(ReportModel reportModel)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {StaticHandler.Token ?? ""}");
                httpClient.Timeout = new TimeSpan(hours: 0, minutes: 10, seconds: 0);
                var content = new StringContent(JsonConvert.SerializeObject(reportModel), System.Text.Encoding.UTF8);
                var url = baseUrl + "/Report/create";

                var mValue = new MediaTypeHeaderValue("application/json");
                content.Headers.ContentType = mValue;
                var response = await httpClient.PutAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
