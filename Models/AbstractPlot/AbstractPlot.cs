using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StravaViewer.Models.AbstractPlot
{
    public abstract class AbstractPlot
    {

        protected List<Activity> activities;
        protected DateTime fromDate;
        protected DateTime toDate;
        //protected TimePeriod timePeriod;
        public List<ActivityCollection> activityCollections = new List<ActivityCollection>();

        public PlotData PlotData = PlotData.Empty();

        abstract protected List<ActivityCollection> GetCollections();
        abstract public string[] GetLabels();

        public AbstractPlot(List<Activity> activities, TimePeriod timePeriod)
        {
            this.activities = activities;
            this.fromDate = timePeriod.StartTime;
            this.toDate = timePeriod.EndTime;

            //this.activityCollections = GetCollections();
        }

        public List<double[]> GetValues()
        {
            List<double[]> valueSeries = new List<double[]>();
            List<double> values_list = new List<double>();

            foreach (ActivityCollection collection in activityCollections)
            {
                values_list.Add(collection.GetSumDistance());          // convert return value to double????
            }

            valueSeries.Add(values_list.ToArray());
            return valueSeries;
        }

        //protected void SetPlotData()
        //{

        //}



    }
}
