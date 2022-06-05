using System;
using Newtonsoft.Json.Linq;
using StravaViewer.Models.AbstractPlot;

namespace StravaViewer.Models
{
    public enum ActivityType
    {
        Run,
        Ride,
        Hike,
        WeightTraining,
        Swim,
        Walk,
        Workout
    }

    public class Activity
    {
        public string id;
        public string name;
        public float distance;
        public float moving_time;
        public float elapsed_time;
        public DateTime start_date;
        public float total_elevation_gain;
        public ActivityType type;
        private BoundingRectangle boundingRectangle = BoundingRectangle.Empty();

        public Activity(JObject json)
        {
            #pragma warning disable CS8602 // Dereference of a possibly null reference.
            this.id = json["id"].ToString();
            this.name = json["name"].ToString();
            this.distance = json["distance"].ToObject<float>();
            this.moving_time = json["moving_time"].ToObject<float>();
            this.elapsed_time = json["elapsed_time"].ToObject<float>();            
            this.total_elevation_gain = json["total_elevation_gain"].ToObject<float>();

            string start_date_string = json["start_date"].ToString();
            start_date = Convert.ToDateTime(start_date_string);

            string type_string = json["type"].ToString();
            type = (ActivityType)Enum.Parse(typeof(ActivityType), type_string);
            #pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        public BoundingRectangle BoundingRectangle
        {
            get
            {
                return boundingRectangle;
            }

            set
            {
                this.boundingRectangle = value;
            }
        }

        public override string ToString()
        {
            return String.Format("{0} - {1} km", name, distance/1000);
        }
    }



}
