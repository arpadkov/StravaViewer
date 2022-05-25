namespace StravaViewer.Models.AbstractPlot
{
    public class AbstractDetailPlot
    {
        private List<Activity> activities;
        private DateTime fromDate;
        private DateTime toDate;

        private List<DateTime> days;
        private List<DetailActivityCollection> activityCollections;

        public AbstractDetailPlot(List<Activity> activities, TimePeriod timePeriod)
        {
            this.activities = activities;
            this.fromDate = timePeriod.StartTime;
            this.toDate = timePeriod.EndTime;

            this.days = GetDays();
            this.activityCollections = GetCollections();
        }

        private List<DateTime> GetDays()
        {
            List<DateTime> days = new List<DateTime>();

            DateTime day = fromDate;
            while (day <= toDate)
            {
                days.Add(day);
                day = day.AddDays(1);
            }
            return days;
        }

        private List<DetailActivityCollection> GetCollections()
        {
            List<DetailActivityCollection> collections = new List<DetailActivityCollection>();

            foreach (DateTime day in days)
            {
                List<Activity> acts = ActivitySorter.GetActsByDay(activities, day);
                collections.Add(new DetailActivityCollection(acts));
            }

            return collections;
        }

        private int MaxActivityCount()
        {
            int count = 0;
            foreach (DetailActivityCollection collection in activityCollections)
            {
                if (collection.Count > count) 
                {
                    count = collection.Count;
                }
            }

            return count;
        }



        public List<double[]> GetValues()
        {
            List<double[]> value_series = new List<double[]>();

            return value_series;
        }

        public string[] GetLabels()
        {
            List<string> labels_list = new List<string>();

            foreach (DateTime day in GetDays())
            {
                labels_list.Add(day.ToString("MMM/dd"));
            }

            string[] labels = labels_list.ToArray();
            return labels;
        }
    }
}
