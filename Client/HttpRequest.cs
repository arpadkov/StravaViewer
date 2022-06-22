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

        public static void PostWithFileAndAuth(string url, Dictionary<string, string> payload, string access_token, string filename)
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", access_token);

            var multipartFormContent = new MultipartFormDataContent();

            //Add other fields
            multipartFormContent.Add(new StringContent("TestActAuto"), name: "name");
            multipartFormContent.Add(new StringContent("tcx"), name: "data_type");

            //Add the file
            var fileStreamContent = new StreamContent(File.OpenRead(filename));
            //fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            multipartFormContent.Add(fileStreamContent, name: "file", fileName: "act.tcx");

            //Send it
            //var response = await client.PostAsync("https://localhost:12345/files/", multipartFormContent);
            //response.EnsureSuccessStatusCode();
            //return await response.Content.ReadAsStringAsync();


            //var payload_string = new StringContent
            //    (
            //    JsonConvert.SerializeObject(payload, Formatting.Indented), Encoding.UTF8, "application/json"
            //    );

            var response = client.PostAsync(url, multipartFormContent).Result;

            int i = 5;


        }
    }
}
