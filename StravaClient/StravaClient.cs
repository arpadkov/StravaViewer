using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace StravaViewer.Client
{
    public class StravaClient
    {
        private HttpClient client = new HttpClient();
        private string application_folder_name = "StravaClient";
        private string authentication_url = "https://www.strava.com/oauth/token";
        private string activites_base_url = "https://www.strava.com/api/v3/athlete/activities?per_page=100";

        string? access_token;
        StravaUserCredentials? user_credentials;        

        private void ReadUserCredentials(string user)
        {
            string applicationPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), application_folder_name);
            string filename = Path.Combine(applicationPath, user + ".json");
            this.user_credentials = JsonConvert.DeserializeObject<StravaUserCredentials>(File.ReadAllText(filename));
        }

        public void SetAccesToken(string user)
        {
            ReadUserCredentials(user);

            Dictionary<string, string> payload_dict = new Dictionary<string, string>
                {
                    {"client_id", user_credentials.ClientId},
                    {"client_secret", user_credentials.ClientSecret},
                    {"refresh_token", user_credentials.RefreshToken},
                    {"grant_type", "refresh_token"},
                };
            var payload = new StringContent(JsonConvert.SerializeObject(payload_dict, Formatting.Indented), Encoding.UTF8, "application/json");

            Console.WriteLine("Requesting acces token ...");
            var response = client.PostAsync(authentication_url, payload).Result;

            var result = response.Content.ReadAsStringAsync().Result;
            JObject result_json = Newtonsoft.Json.Linq.JObject.Parse(result);

            #pragma warning disable CS8602 // Dereference of a possibly null reference.
            this.access_token = result_json["access_token"].ToString();
            #pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        private JArray GetActivities(int page)
        {
            //var activities = new List<Activity>();
            Console.WriteLine(String.Format("Requesting activities for page {0}...", page));

            string activites_url = activites_base_url + "&page=" + page.ToString();

            var request = new HttpRequestMessage(HttpMethod.Get, activites_url);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", access_token);
            var response = client.SendAsync(request).Result;

            var activities_json = JArray.Parse(response.Content.ReadAsStringAsync().Result);

            return activities_json;
        }


        public JArray GetAllActivities()
        {
            JArray activities_json = new JArray();
            JArray new_activities_json = new JArray();
            bool page_has_data = true;
            int page = 1;

            while (page_has_data)
            {
                new_activities_json = GetActivities(page);
                activities_json.Merge(new_activities_json);

                if (new_activities_json.Count > 0)
                {
                    page_has_data = false; // CHANGE BACK TO TRUE !!!!!!!!!!!!!!!!!!!
                }
                else
                {
                    page_has_data = false; 
                }

                page++;
            }

            return activities_json;
        }

    }
}
