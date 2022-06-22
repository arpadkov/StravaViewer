using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace StravaViewer.Client
{
    internal class HttpRequest
    {
        private static HttpClient client = new HttpClient();

        public static string Post(string url, Dictionary<string, string> payload, string result_key)
        {
            string selected_result;

            var payload_string = new StringContent
                (
                JsonConvert.SerializeObject(payload, Formatting.Indented), Encoding.UTF8, "application/json"
                );

            var response = client.PostAsync(url, payload_string).Result;

            var result = response.Content.ReadAsStringAsync().Result;
            JObject result_json = Newtonsoft.Json.Linq.JObject.Parse(result);

            #pragma warning disable CS8602 // Dereference of a possibly null reference.
            selected_result = result_json[result_key].ToString();
            #pragma warning restore CS8602 // Dereference of a possibly null reference.

            return selected_result;
        }

        public static string GetWithToken(string url, string token)
        {
            string result;

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = client.SendAsync(request).Result;

            result = response.Content.ReadAsStringAsync().Result;


            return result;
        }
    }
}
