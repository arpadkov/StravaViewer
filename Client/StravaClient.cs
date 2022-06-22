using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace StravaViewer.Client
{
    public class StravaClient
    {
        private string user;
        private string clientPath;
        private string userFolderPath;

        //private HttpClient client = new HttpClient();
        private string application_folder_name = "StravaClient";
        private string authentication_url = "https://www.strava.com/oauth/token";
        private string activites_base_url = "https://www.strava.com/api/v3/athlete/activities?per_page=100";

        string? access_token;
        StravaUserCredentials? user_credentials;
        
        public StravaClient(string user)
        {
            this.user = user;

            this.clientPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), application_folder_name);
            this.userFolderPath = Path.Combine(clientPath, user);
        }

        public JArray GetAllActivities(bool sync = false)
        {
            JArray activities_json = new JArray();

            CheckBackupFolder();

            if (sync || IsEmptyUserBackupFolder())
            {
                SetUserCredentials();
                SetAccesToken();
                GetAllActivitiesFromAPI();
            }

            string[] json_files = Directory.GetFiles(userFolderPath);

            foreach (string json_file in json_files)
            {
                activities_json.Add(JObject.Parse(File.ReadAllText(json_file)));
            }

            return activities_json;
        }

        private void SetUserCredentials()
        {
            string filename = Path.Combine(clientPath, user + ".json");
            this.user_credentials = JsonConvert.DeserializeObject<StravaUserCredentials>(File.ReadAllText(filename));
        }

        private void SetAccesToken()
        {
            Dictionary<string, string> payload_dict = new Dictionary<string, string>
                {
                    {"client_id", user_credentials.ClientId},
                    {"client_secret", user_credentials.ClientSecret},
                    {"refresh_token", user_credentials.RefreshToken},
                    {"grant_type", "refresh_token"},
                };

            //var payload = new StringContent(JsonConvert.SerializeObject(payload_dict, Formatting.Indented), Encoding.UTF8, "application/json");

            //Console.WriteLine("Requesting acces token ...");
            //var response = client.PostAsync(authentication_url, payload).Result;

            //var result = response.Content.ReadAsStringAsync().Result;
            //JObject result_json = Newtonsoft.Json.Linq.JObject.Parse(result);

            //#pragma warning disable CS8602 // Dereference of a possibly null reference.
            //this.access_token = result_json["access_token"].ToString();
            //#pragma warning restore CS8602 // Dereference of a possibly null reference.

            this.access_token = HttpRequest.Post(authentication_url, payload_dict, "access_token");
        }

        private JArray GetAllActivitiesFromAPI()
        {


            JArray activities_json = new JArray();
            JArray new_activities_json = new JArray();
            bool page_has_data = true;
            int page = 1;

            while (page_has_data)
            {
                new_activities_json = GetActivitiesByPage(page);
                activities_json.Merge(new_activities_json);

                if (new_activities_json.Count > 0)
                {
                    page_has_data = true;
                }
                else
                {
                    page_has_data = false;
                }

                page++;
            }

            SaveAllActivityJson(activities_json);

            return activities_json;
        }

        private JArray GetActivitiesByPage(int page)
        {
            string activites_url = activites_base_url + "&page=" + page.ToString();

            //var request = new HttpRequestMessage(HttpMethod.Get, activites_url);
            //request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", access_token);
            //var response = client.SendAsync(request).Result;

            //var activities_json = JArray.Parse(response.Content.ReadAsStringAsync().Result);

            string activities_string = HttpRequest.GetWithToken(activites_url, access_token);
            var activities_json = JArray.Parse(activities_string);

            return activities_json;
        }

        private void CheckBackupFolder()
        {
            // If directory does not exist, create it
            if (Directory.Exists(userFolderPath) == false)
            {
                Directory.CreateDirectory(userFolderPath);
            }
        }

        private bool IsEmptyUserBackupFolder()
        {
            return !Directory.EnumerateFileSystemEntries(userFolderPath).Any();
        }

        private void SaveAllActivityJson(JArray activities_json)
        {
            string actId;
            string raw_date_string;
            DateTime start_date;
            string date;
            string filename;
            string unformattedJson;
            string formattedJson;

            foreach (var activity_json in activities_json)
            {
                #pragma warning disable CS8602 // Dereference of a possibly null reference.
                actId = activity_json["id"].ToString();
                raw_date_string = activity_json["start_date"].ToString();
                #pragma warning restore CS8602 // Dereference of a possibly null reference.

                start_date = Convert.ToDateTime(raw_date_string);
                date = start_date.ToString("yyyy-MM-dd");
                

                filename = Path.Combine(userFolderPath, date + "_" + actId + ".json");

                unformattedJson = JsonConvert.SerializeObject(activity_json);
                formattedJson = JValue.Parse(unformattedJson).ToString(Formatting.Indented);
                File.WriteAllText(filename, formattedJson);
            }
        }

        public void GetActivityStream()
        {
            string act_id = "7205310239";

            string stream_url = "https://www.strava.com/api/v3/activities/7205310239/streams?keys=&key_by_type=";
        }

        https://www.strava.com/api/v3/activities/{id}/streams?keys=&key_by_type=" "Authorization: Bearer [[token]]

        https://www.strava.com/api/v3/activities/{id}?include_all_efforts=" "Authorization: Bearer [[token]]
    }
}
