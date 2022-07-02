using StravaViewer.Client.Activity;

namespace StravaViewer.Models.AbstractPlot
{
    public class ActivityCollection
    {
        public List<Activity> activities;
        private BoundingRectangle? boundingRectangle;

        public ActivityCollection(List<Activity> activities)
        {
            this.activities = activities;
        }

        public int Count
        {
            get { return this.activities.Count; }
            set {; }
        }

        public float GetTotalValue(InfoType type)
        {

            if (activities.Any())
            {
                switch (type)
                {
                    case InfoType.Distance: return activities.Sum(act => act.distance) / 1000;
                    case InfoType.ElevationGain: return activities.Sum(act => act.total_elevation_gain);
                    default: return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        /*
         * relevant for DetailPlot
         * returns the distance for the "level"-th activity
         */
        public float GetValue(int level, InfoType type)
        {
            if (activities.Count != 0 && activities.Count > level)
            {


                switch (type)
                {
                    case InfoType.Distance: return activities[level].distance / 1000;
                    case InfoType.ElevationGain: return activities[level].total_elevation_gain;
                    default: return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        public BoundingRectangle BoundingRectangle
        {
            get
            {
                if (boundingRectangle != null)
                    return boundingRectangle;
                return BoundingRectangle.Empty();
            }

            set
            {
                this.boundingRectangle = value;
            }
        }

        public override string ToString()
        {
            return String.Format("{0} Activities, {1} km total", activities.Count, GetTotalValue(InfoType.Distance));
        }

        public string Name
        {
            get
            {
                return "Activities";
            }

            set { }
        }

        public string Distance
        {
            get
            {
                double distance_km = Math.Round(GetTotalValue(InfoType.Distance), 2);
                return distance_km.ToString() + " km";
            }

            set { }
        }

        public string ElevationGain
        {
            get
            {
                return Math.Round(GetTotalValue(InfoType.ElevationGain), 2).ToString() + " m";
            }

            set { }
        }

        public string MovingDuration
        {
            get
            {
                TimeSpan duration = TimeSpan.FromSeconds(GetTotalValue(InfoType.Movingtime));
                return duration.ToString();
            }

            set { }
        }

        public string Date
        {
            get
            {
                return activities[0].start_date.ToString("yyyy-MMM") + " - " + activities[activities.Count-1].start_date.ToString("yyyy-MMM");
            }

            set { }
        }

    }
}
