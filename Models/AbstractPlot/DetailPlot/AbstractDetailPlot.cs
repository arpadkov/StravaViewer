﻿using StravaViewer.Client.Activity;

namespace StravaViewer.Models.AbstractPlot
{
    public class AbstractDetailPlot : AbstractPlot
    {
        private List<DateTime> days;

        public AbstractDetailPlot(List<Activity> activities, TimePeriod timePeriod, InfoType type) : base(activities, timePeriod, type)
        {
            this.days = GetDays();
            this.activityCollections = GetCollections();
            this.PlotData = new PlotData(GetValues(), GetLabels(), GetTitle());
            SetBoundingRectangles();
            //SetActivities();
        }

        private List<DateTime> GetDays()
        {
            List<DateTime> days = new List<DateTime>();

            DateTime day = fromDate;
            while (day < toDate)
            {
                days.Add(day);
                day = day.AddDays(1);
            }
            return days;
        }

        protected override List<ActivityCollection> GetCollections()
        {
            List<ActivityCollection> collections = new List<ActivityCollection>();

            foreach (DateTime day in days)
            {
                List<Activity> acts = ActivitySorter.GetActsByDay(activities, day);
                collections.Add(new ActivityCollection(acts));
            }

            return collections;
        }

        private int MaxActivityCount()
        {
            int count = 0;
            foreach (ActivityCollection collection in activityCollections)
            {
                if (collection.Count > count) 
                {
                    count = collection.Count;
                }
            }

            return count;
        }

        public new List<double[]> GetValues()
        {
            List<double[]> value_series = new List<double[]>();

            for (int level=0; level<MaxActivityCount(); level++)
            {
                List<double> values = new List<double>();
                foreach (ActivityCollection collection in activityCollections)
                {
                    values.Add(collection.GetValue(level, type));
                }
                value_series.Add(values.ToArray());
            }

            if (value_series.Count == 0)
            {
                double[] empty_array = new double[activityCollections.Count];
                value_series.Add(empty_array);
            }

            return ConvertValues(value_series);


        }

        /*
         * converts the values:
         * to simulate stacking, shifts values
         * TERRIBLE
         */
        private List<double[]> ConvertValues(List<double[]> valueSeries)
        {
            List<double[]> result = new List<double[]>();

            double[] values_offset = new double[valueSeries[0].Length];
            for (int i = 0; i < valueSeries.Count; i++)
            {

                double[] new_values = offsetSeries(valueSeries[i], values_offset);

                values_offset = new_values;

                result.Add(new_values);
            }

            return result;
        }

        private double[] offsetSeries(double[] original, double[] offset)
        {
            double[] result = new double[original.Length];
            for (int i = 0; i < original.Length; i++)
            {
                result[i] = original[i] + offset[i];
            }
            return result;
        }

        public override string[] GetLabels()
        {
            List<string> labels_list = new List<string>();

            foreach (DateTime day in GetDays())
            {
                labels_list.Add(day.ToString("dd"));
            }

            string[] labels = labels_list.ToArray();
            return labels;
        }

        public override string GetTitle()
        {
            return fromDate.ToString("yyyy - MMMM");
        }

        private void SetBoundingRectangles()
        {
            double bottom;

            // for each column/ActivityCollection
            foreach (var (collection, indexCol) in activityCollections.Select((valueColl, iColl) => (valueColl, iColl)))
            {
                // for each Activity in the Collection
                foreach (var (activity, indexAct) in collection.activities.Select((valueAct, iAct) => (valueAct, iAct)))
                {
                    // Top of the column minus the height
                    bottom = PlotData.valueSeries[indexAct][indexCol] - activity.GetValue(type);

                    activity.BoundingRectangle = new BoundingRectangle(
                    height: activity.GetValue(type),
                    verticalCenter: indexCol,
                    bottom: bottom,
                    width: 0.9);
                }                
            }
        }
    }
}
