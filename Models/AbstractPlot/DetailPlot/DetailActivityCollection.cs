namespace StravaViewer.Models.AbstractPlot
{
    internal class DetailActivityCollection
    {
        private List<Activity> activities;

        public DetailActivityCollection(List<Activity> activities)
        {
            this.activities = activities;
        }

        public int Count
        {
            get { return this.activities.Count; }
            set { ; }
        }
    }
}
