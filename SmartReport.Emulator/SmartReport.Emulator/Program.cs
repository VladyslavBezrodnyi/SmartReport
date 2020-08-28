using System;
using System.Net.Http;

namespace SmartReport.Emulator
{
    class Program
    {
        static readonly HttpClient client = new HttpClient();
        static readonly string url = "https://smart-report-backend.azurewebsites.net/api/Account/visit/";
        //static readonly string url = "https://localhost:2000/api/Account/visit/";
        static void Main(string[] args)
        {
            Run().Wait();
        }

        static async System.Threading.Tasks.Task Run()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter user id:");
                    string id = Console.ReadLine();
                    if (id == "0")
                    {
                        break;
                    }
                    string RequestURL = url + id;
                  //  HttpResponseMessage response = await client.GetAsync(RequestURL);
                 //   response.EnsureSuccessStatusCode();
                   // string responseBody = await response.Content.ReadAsStringAsync();
                    string responseBody = await client.GetStringAsync(RequestURL);
                    Console.WriteLine(responseBody);
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine("Message :{0} ", e.Message);
                }
            }
        }

    }
}
