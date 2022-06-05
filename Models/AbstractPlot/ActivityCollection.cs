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

        public float GetSumDistance()
        {
            if (activities.Any())
            {
                return activities.Sum(act => act.distance) / 1000;
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
            return String.Format("{0} Activities, {1} km total", activities.Count, GetSumDistance());
        }

    }
}
