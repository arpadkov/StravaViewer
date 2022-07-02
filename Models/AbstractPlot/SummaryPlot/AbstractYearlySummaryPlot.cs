using StravaViewer.Client.Activity;
using System;

namespace StravaViewer.Models.AbstractPlot
{
    internal class AbstractYearlySummaryPlot : AbstractPlot
    {
        private List<int> years;

        public AbstractYearlySummaryPlot(List<Activity> activities, TimePeriod timePeriod, InfoType type) : base(activities, timePeriod, type)
        {
            this.years = getYears();
            this.activityCollections = GetCollections();
            this.PlotData = new PlotData(GetValues(), GetLabels(), GetTitle());
            SetBoundingRectangles();
        }

        private List<int> getYears()
        {
            List<int> years = new List<int>();
            DateTime date = fromDate;

            while (date.Year <= toDate.Year)
            {
                years.Add(date.Year);           // TODO: check if .Year returns int
                date = date.AddYears(1);
            }

            return years;
        }

        protected override List<ActivityCollection> GetCollections()
        {
            List<ActivityCollection> collections = new List<ActivityCollection>();

            foreach (int year in years)
            {
                DateTime firstDay = new DateTime(year, 1, 1);
                DateTime lastDay = new DateTime(year + 1, 1, 1);

                TimePeriod currentYear = new TimePeriod(firstDay, lastDay);

                List<Activity> acts = ActivitySorter.GetActsByDate(activities, currentYear);
                collections.Add(new ActivityCollection(acts));
            }

            return collections;
        }

        private void SetBoundingRectangles()
        {
            foreach (var (collection, index) in activityCollections.Select((value, i) => (value, i)))
            {
                collection.BoundingRectangle = new BoundingRectangle(
                    height: collection.GetTotalValue(type),
                    verticalCenter: index,
                    bottom: 0,
                    width: 0.9);
            }
        }

        public override string[] GetLabels()
        {
            List<string> labels_list = new List<string>();

            foreach (int year in years)
            {
                labels_list.Add(year.ToString());
            }

            string[] labels = labels_list.ToArray();
            return labels;
        }

        public override string GetTitle()
        {
            return "All years";
        }

    }
}
