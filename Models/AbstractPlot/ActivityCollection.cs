namespace StravaViewer.Models.AbstractPlot
{
    internal class ActivityCollection
    {
        private List<Activity> activities;

        public ActivityCollection(List<Activity> activities)
        {
            this.activities = activities;
        }

        public float getDistance()
        {
            return activities.Sum(act => act.distance)/1000;
        }

    }
}
