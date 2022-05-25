namespace StravaViewer.Models.AbstractPlot
{
    public class ActivityCollection
    {
        private List<Activity> activities;

        public ActivityCollection(List<Activity> activities)
        {
            this.activities = activities;
        }

        public float getDistance()
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

    }
}
