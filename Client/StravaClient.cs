using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StravaViewer.Client.Activity;
using System.Data;

namespace StravaViewer.Client
{
    public class StravaClient
    {
        private string user;
        private string clientPath;
        private string userFolderPath;

        //private HttpClient client = new HttpClient();
        private string application_folder_name = "StravaClient";
        private string upload_folder_name = "to_upload";
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

        public JArray GetAllActivities(bool fullInit = false)
        {
            JArray activities_json = new JArray();

            CheckBackupFolder();
            try
            {
                SetAccesToken();
            }
            catch
            {
                // TODO
            }
            

            if (fullInit || IsEmptyUserBackupFolder())
            {
                GetAllActivitiesFromAPI();
            }
            else
            {
                GetLastActivitiesFromAPI();
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

        public void SetAccesToken()
        {
            SetUserCredentials();

            Dictionary<string, string> payload_dict = new Dictionary<string, string>
                {
                    {"client_id", user_credentials.ClientId},
                    {"client_secret", user_credentials.ClientSecret},
                    {"refresh_token", user_credentials.RefreshToken},
                    {"grant_type", "refresh_token"},
                };

            this.access_token = HttpRequest.Post(authentication_url, payload_dict, "access_token");
        }

        // TODO: is the return needed?
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
                //if (page > 10)
                //{
                //    wait(60000);
                //}
            }

            SaveAllActivityJson(activities_json);

            return activities_json;
        }

        // TODO: is the return needed?
        private JArray GetLastActivitiesFromAPI()
        {
            JArray activities_json = new JArray();
            activities_json = GetActivitiesByPage(1);



            SaveAllActivityJson(activities_json);

            return activities_json;
        }

        private JArray GetActivitiesByPage(int page)
        {
            string activites_url = activites_base_url + "&page=" + page.ToString();

            Dictionary<string, string> payload_dict = new Dictionary<string, string> { };

            string activities_string = HttpRequest.GetWithToken(activites_url, payload_dict, access_token);
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

                // In case the Activity already exists, it will be overwritten
                File.WriteAllText(filename, formattedJson);
            }
        }


        public JArray GetActivityStream(string act_id, string stream)
        {
            // TODO: Streams: in url eg: /streams?keys=heartrate,time
            string stream_url = "https://www.strava.com/api/v3/activities/" + act_id + "/streams?";

            Dictionary<string, string> payload_dict = new Dictionary<string, string>
                {
                    {"keys", stream},
                    {"key_by_type", "true"},
                };

            string response = HttpRequest.GetWithToken(stream_url, payload_dict, access_token);
            var json_data = JObject.Parse(response)[stream]["data"] as JArray;

            return json_data;
        }

        public Dictionary<string, JArray> GetActivityStreams(string act_id, List<string> streams)
        {
            // TODO: Should return ActivityStreams object
            string stream_url = "https://www.strava.com/api/v3/activities/" + act_id + "/streams?";

            string keys = "";
            foreach (string key in streams)
            {
                keys += key + ",";
            }

            Dictionary<string, string> payload_dict = new Dictionary<string, string>
                {
                    {"keys", keys},
                    {"key_by_type", "true"},
                };

            string response = HttpRequest.GetWithToken(stream_url, payload_dict, access_token);
            var json_data = JObject.Parse(response)[streams[0]];

            var JStreams = new Dictionary<string, JArray>
                {
                    {"latlng", JObject.Parse(response)["latlng"]["data"] as JArray},
                    {"distance", JObject.Parse(response)["distance"]["data"] as JArray},
                    {"elevation", JObject.Parse(response)["altitude"]["data"] as JArray},
                    {"heartrate", JObject.Parse(response)["heartrate"]["data"] as JArray},
                    {"time", JObject.Parse(response)["time"]["data"] as JArray}
                };

            return JStreams;
        }

        public ActivityLaps ListActivityLaps(string act_id)
        {
            

            //DataTable lapsTable = new DataTable("ActivityTable");
            //DataRow row;

            //lapsTable.Columns.Add(new DataColumn("Index", Type.GetType("System.Int32")));
            //lapsTable.Columns.Add(new DataColumn("Distance", Type.GetType("System.Single")));
            //lapsTable.Columns.Add(new DataColumn("Average Pace", Type.GetType("System.Single")));
            //lapsTable.Columns.Add(new DataColumn("Time", Type.GetType("System.Single")));
            //lapsTable.Columns.Add(new DataColumn("Average Heartrate", Type.GetType("System.Single")));

            string laps_url = "https://www.strava.com/api/v3/activities/" + act_id + "/laps?";

            Dictionary<string, string> payload_dict = new Dictionary<string, string>{};

            string response = HttpRequest.GetWithToken(laps_url, payload_dict, access_token);
            var json_data = JArray.Parse(response);

            ActivityLaps Laps = new ActivityLaps(json_data);

            //foreach (var Jlap in json_data)
            //{
            //    row = lapsTable.NewRow();
            //    row["Index"] = Jlap["lap_index"].ToObject<int>();
            //    row["Distance"] = Jlap["distance"].ToObject<float>();
            //    row["Average Pace"] = Jlap["average_speed"].ToObject<float>();
            //    row["Time"] = Jlap["elapsed_time"].ToObject<float>();
            //    row["Average Heartrate"] = Jlap["average_heartrate"].ToObject<float>();
            //    lapsTable.Rows.Add(row);
            //}

            //return lapsTable;
            return Laps;
        }

        private void UploadActivity(string filepath)
        {
            string url = "https://www.strava.com/api/v3/uploads";
            //filename = @"C:\Users\95arp\AppData\Roaming\StravaClient\20220303_TEST.tcx";

            // currently not used
            Dictionary<string, string> payload_dict = new Dictionary<string, string>
                {
                    {"name", "TestActivity"},
                    {"data_type", "tcx"},
                };

            HttpRequest.PostWithFileAndAuth(url, payload_dict, access_token, filepath);
        }

        public void UploadActivities()
        {
            string upload_folder = Path.Combine(userFolderPath, upload_folder_name);

            foreach (string filepath in Directory.GetFiles(upload_folder).Reverse())
            {
                //string filepath = Path.Combine(upload_folder, filename);

                

                Console.WriteLine(filepath);

                UploadActivity(filepath);
                wait(10000);
                File.Delete(filepath);

            }

        }

        private void wait(int milliseconds)
        {
            var timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;

            // Console.WriteLine("start wait timer");
            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();

            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
                // Console.WriteLine("stop wait timer");
            };

            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }
    }
}
