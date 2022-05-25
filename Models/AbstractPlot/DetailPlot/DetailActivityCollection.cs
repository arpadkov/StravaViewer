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
