using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StravaViewer.Models.AbstractPlot
{
    internal class AbstractMonthlySummaryPlot : AbstractPlot
    {
        private List<int> months;

        public AbstractMonthlySummaryPlot(List<Activity> activities, TimePeriod timePeriod) : base(activities, timePeriod)
        {
            this.months = getMonths();
            this.activityCollections = GetCollections();
            this.PlotData = new PlotData(GetValues(), GetLabels());
            SetBoundingRectangles();
        }

        private List<int> getMonths()
        {
            List<int> months = new List<int>();
            DateTime date = fromDate;

            while (date < toDate)
            {
                months.Add(date.Month);           // TODO: check if .Year returns int
                date = date.AddMonths(1);
            }

            return months;
        }

        protected override List<ActivityCollection> GetCollections()
        {
            List<ActivityCollection> collections = new List<ActivityCollection>();

            foreach (int month in months)
            {
                DateTime firstDay = new DateTime(fromDate.Year, month, 1);
                DateTime lastDay = new DateTime(fromDate.Year, month, 1).AddMonths(1);

                TimePeriod currentMonth = new TimePeriod(firstDay, lastDay);

                List<Activity> acts = ActivitySorter.GetActsByDate(activities, currentMonth);
                collections.Add(new ActivityCollection(acts));
            }

            return collections;
        }

        private void SetBoundingRectangles()
        {
            foreach (var (collection, index) in activityCollections.Select((value, i) => (value, i)))
            {
                collection.BoundingRectangle = new BoundingRectangle(
                    height: collection.GetSumDistance(),
                    verticalCenter: index,
                    bottom: 0,
                    width: 0.5);
            }
        }


        public override string[] GetLabels()
        {
            List<string> labels_list = new List<string>();

            foreach (int month in months)
            {
                labels_list.Add(new DateTime(1, month, 1).ToString("MMM"));
            }

            string[] labels = labels_list.ToArray();
            return labels;
        }



    }
}
