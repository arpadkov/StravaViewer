using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StravaViewer.Models.AbstractPlot
{
    public abstract class AbstractSummaryPlot
    {

        protected List<Activity> activities;
        protected DateTime fromDate;
        protected DateTime toDate;
        //protected TimePeriod timePeriod;

        abstract protected List<ActivityCollection> GetCollections(); 
        abstract public string[] GetLabels();

        public AbstractSummaryPlot(List<Activity> activities, TimePeriod timePeriod)
        {
            this.activities = activities;
            this.fromDate = timePeriod.StartTime;
            this.toDate = timePeriod.EndTime;
        }

        public double[] GetValues()
        {
            List<double> values_list = new List<double>();

            foreach (ActivityCollection collection in GetCollections())
            {
                values_list.Add(collection.GetSumDistance());          // convert return value to double????
            }

            double[] values = values_list.ToArray();
            return values;
        }



    }
}
