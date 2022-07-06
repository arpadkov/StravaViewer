using Newtonsoft.Json.Linq;

namespace StravaViewer.Client.Activity
{
    public class ActivityLap
    {
        public int index;
        public TimeSpan time;
        public DateTime start_date;
        public float distance;
        public float elevation_gain;
        public TimeSpan average_pace;
        public float max_speed;
        public float average_heartrate;
        public float max_heartrate;
        public int start_index;
        public int end_index;

        public ActivityLap(JObject Jlap)
        {
            index = Jlap["lap_index"].ToObject<int>();
            time = TimeSpan.FromSeconds(Jlap["elapsed_time"].ToObject<int>());
            start_date = Convert.ToDateTime(Jlap["start_date_local"].ToString());
            distance = Jlap["distance"].ToObject<float>();
            elevation_gain = Jlap["total_elevation_gain"].ToObject<float>();

            float pace_ms = Jlap["average_speed"].ToObject<float>();
            float pace_skm = 1000 / pace_ms;
            average_pace = TimeSpan.FromSeconds(pace_skm);

            max_speed = Jlap["max_speed"].ToObject<float>();
            average_heartrate = Jlap["average_heartrate"].ToObject<float>();
            max_heartrate = Jlap["max_heartrate"].ToObject<float>();
            start_index = Jlap["start_index"].ToObject<int>();
            end_index = Jlap["end_index"].ToObject<int>();
        }
    }
}
