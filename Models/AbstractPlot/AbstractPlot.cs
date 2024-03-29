﻿using StravaViewer.Client.Activity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StravaViewer.Models.AbstractPlot
{
    public abstract class AbstractPlot
    {

        public List<Activity> activities;
        protected DateTime fromDate;
        protected DateTime toDate;
        //protected TimePeriod timePeriod;
        public List<ActivityCollection> activityCollections = new List<ActivityCollection>();

        public PlotData PlotData = PlotData.Empty();
        protected InfoType type;

        abstract protected List<ActivityCollection> GetCollections();
        abstract public string[] GetLabels();
        abstract public string GetTitle();

        public AbstractPlot(List<Activity> activities, TimePeriod timePeriod, InfoType type)
        {
            this.activities = activities;
            this.fromDate = timePeriod.StartTime;
            this.toDate = timePeriod.EndTime;
            this.type = type;

            //this.activityCollections = GetCollections();
        }

        public List<double[]> GetValues()
        {
            List<double[]> valueSeries = new List<double[]>();
            List<double> values_list = new List<double>();

            foreach (ActivityCollection collection in activityCollections)
            {
                values_list.Add(collection.GetTotalValue(type));          // convert return value to double????
            }

            valueSeries.Add(values_list.ToArray());
            return valueSeries;
        }

        //protected void SetPlotData()
        //{

        //}



    }
}
