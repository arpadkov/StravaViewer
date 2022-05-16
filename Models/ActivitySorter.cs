namespace StravaViewer.Models
{
    internal class ActivitySorter
    {
        // TODO optimization: send list reference and remove acts from the list
        public static List<Activity> GetActsByDate(List<Activity> activities, TimePeriod timePeriod)
        {

            DateTime from_date = timePeriod.StartTime.Date + new TimeSpan(0, 0, 0);
            DateTime to_date = timePeriod.EndTime.Date + new TimeSpan(23, 59, 59);

            List<Activity> selected_activities = new List<Activity>();

            foreach (Activity activity in activities)
            {
                if (activity.start_date > from_date && activity.start_date < to_date)
                {
                    selected_activities.Add(activity);
                }
            }

            return selected_activities;
        }
    }
}
