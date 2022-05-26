namespace StravaViewer.Models.AbstractPlot
{
    public class ActivityCollection
    {
        private List<Activity> activities;

        public ActivityCollection(List<Activity> activities)
        {
            this.activities = activities;
        }

        public int Count
        {
            get { return this.activities.Count; }
            set {; }
        }

        public float GetSumDistance()
        {
            if (activities.Any())
            {
                return activities.Sum(act => act.distance)/1000;
            }
            else
            {
                return 0;
            }
            
        }

        public float GetDistance(int level)
        {
            if (activities.Count != 0 && activities.Count > level)
            {
                return activities[level].distance / 1000;
            }
            else
            {
                return 0;
            }

        }

    }
}
