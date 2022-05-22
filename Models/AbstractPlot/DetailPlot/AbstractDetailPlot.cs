using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StravaViewer.Models.AbstractPlot.DetailPlot
{
    internal class AbstractDetailPlot
    {
        private List<Activity> activities;
        private DateTime fromDate;
        private DateTime toDate;

        public AbstractDetailPlot(List<Activity> activities, TimePeriod timePeriod)
        {
            this.activities = activities;
            this.fromDate = timePeriod.StartTime;
            this.toDate = timePeriod.EndTime;
        }

        private List<DateTime> GetDays()
        {
            List<DateTime> days = new List<DateTime>();

            for (DateTime day = fromDate; day <= toDate; day.AddDays(1))
            {
                days.Add(day);
            }
            return days;
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
