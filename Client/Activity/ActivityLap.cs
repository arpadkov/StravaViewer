using Newtonsoft.Json.Linq;

namespace StravaViewer.Client.Activity
{
    internal class ActivityLap
    {
        int index;
        TimeSpan time;
        DateTime start_date;
        float distance;
        float elevation_gain;
        float average_speed;
        float max_speed;
        float average_heartrate;
        float max_heartrate;
        int start_index;
        int end_index;

        public ActivityLap(JObject Jlap)
        {
            index = Jlap["lap_index"].ToObject<int>();
            time = TimeSpan.FromSeconds(Jlap["elapsed_time"].ToObject<int>());
            start_date = Convert.ToDateTime(Jlap["start_date_local"].ToString());
            distance = Jlap["distance"].ToObject<float>();
            elevation_gain = Jlap["total_elevation_gain"].ToObject<float>();
            average_speed = Jlap["average_speed"].ToObject<float>();
            max_speed = Jlap["max_speed"].ToObject<float>();
            average_heartrate = Jlap["average_heartrate"].ToObject<float>();
            max_heartrate = Jlap["max_heartrate"].ToObject<float>();
            start_index = Jlap["start_index"].ToObject<int>();
            end_index = Jlap["end_index"].ToObject<int>();
        }
    }
}
