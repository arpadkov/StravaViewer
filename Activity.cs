using Newtonsoft.Json.Linq;

namespace StravaViewer.Models
{
    public class Activity
    {
        public string id;
        public string name;
        public float distance;
        public float moving_time;
        public float elapsed_time;
        public string start_date;
        public float total_elevation_gain;

        public Activity(JObject json)
        {
            #pragma warning disable CS8602 // Dereference of a possibly null reference.
            this.id = json["id"].ToString();
            this.name = json["name"].ToString();
            this.distance = json["distance"].ToObject<float>();
            this.moving_time = json["moving_time"].ToObject<float>();
            this.elapsed_time = json["elapsed_time"].ToObject<float>();
            this.start_date = json["start_date"].ToString();
            this.total_elevation_gain = json["total_elevation_gain"].ToObject<float>();
            #pragma warning restore CS8602 // Dereference of a possibly null reference.

            Console.WriteLine(String.Format("Construction activity ID - {0}", id));
        }
    }

}
