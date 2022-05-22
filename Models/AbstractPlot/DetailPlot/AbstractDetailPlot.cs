using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StravaViewer.Models.AbstractPlot
{
    public class AbstractDetailPlot
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

        //not ready
        //this is baaaad
        public List<double[]> GetValues()
        {
            List<double[]> value_series = new List<double[]>();

            int level = 0;
            List<Activity> remaining_acts = new List<Activity>(activities);

            while (remaining_acts.Count > 0)
            {
                value_series.Add(GetValuesForLevel(remaining_acts));
                level++;
            }

            return value_series;
        }

        private double[] GetValuesForLevel(List<Activity> remaining_acts)
        {
            double[] values = new double[remaining_acts.Count];

            int i = 0;
            foreach (DateTime day in GetDays())
            {
                foreach (Activity activity in remaining_acts)
                {
                    if (activity.start_date.Day == day.Day)
                    {
                        values[i] = activity.distance;

                    }
                }
                i++;
            }

            return values;
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
