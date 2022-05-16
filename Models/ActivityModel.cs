using Newtonsoft.Json.Linq;
using StravaViewer.Client;
using StravaViewer.Models.AbstractPlot;

namespace StravaViewer.Models
{
    public enum InfoType
    {
        Distance,
        ElvationgGain,
        Movingtime
    }

    public enum PlotType
    {
        YearlySummary,
        MonthlySummary,
        MonthDetail,
        UniqueDetail,
    }

    public class ActivityModel
    {
        public event EventHandler? ActivitiesSet;
        public event EventHandler? ModelChanged;
        
        List<Activity> activities;
        StravaClient Client;

        private ActivityType activityType;
        private InfoType infoType;
        private PlotType plotType;

        private TimePeriod displayTime;

        public ActivityModel()
        {
            this.displayTime = new TimePeriod(DateTime.Now, DateTime.Now);
            //this.displayTime = TimePeriod.FromYear(2019);

            this.activityType = ActivityType.Run;
            this.infoType = InfoType.Distance;
            this.plotType = PlotType.MonthlySummary;

            this.activities = new List<Activity>();
            this.Client = new StravaClient();
        }

        public TimePeriod DisplayTime
        {
            get { return this.displayTime; }
            set { this.displayTime = value; OnModelChange(EventArgs.Empty); }
        }

        public PlotType PlotType
        {
            get { return this.plotType; }
            set { this.plotType = value; OnModelChange(EventArgs.Empty); }
        }

        public void SetActivities()
        {
            Client.SetAccesToken("95.arpadkov");

            JArray json_activities = Client.GetAllActivities();

            foreach (JObject json_activity in json_activities)
                {
                activities.Add(new Activity(json_activity));
                }

            OnActivitiesSet(EventArgs.Empty);
        }

        private List<Activity> getActivitiesByType()
        {
            List<Activity> acts = new List<Activity>();

            foreach (Activity act in activities)
            {
                if (act.type == activityType)
                {
                    acts.Add(act);
                }
            }

            return acts;
        }

        public PlotData GetPlotData()
        {
            if (plotType == PlotType.YearlySummary)
            {
                return getYearlySummaryPlot();
            }
            else if (plotType == PlotType.MonthlySummary)
            {
                return getMonthlySummaryPlot();
            }

            else
            {
                return PlotData.Empty();
            }
        }

        private PlotData getMonthlySummaryPlot()
        {
            int display_year = displayTime.EndYear;
            displayTime = TimePeriod.FromYear(display_year);

            //var fromDate = new DateTime(2021, 1, 1);
            //var toDate = new DateTime(2022, 1, 1);

            //TimePeriod month = new TimePeriod(displayTime, toDate);

            var abstract_plot = new AbstractMonthlySummaryPlot(getActivitiesByType(), displayTime);

            var plot_data = new PlotData(abstract_plot.GetValues(), abstract_plot.GetLabels());
            return plot_data;
        }

        private PlotData getYearlySummaryPlot()
        {
            //var fromDate = firstAct().start_date;
            //var toDate = lastAct().start_date;
            displayTime = new TimePeriod(firstAct().start_date, lastAct().start_date);

            var abstract_plot = new AbstractYearlySummaryPlot(getActivitiesByType(), displayTime);

            var plot_data = new PlotData(abstract_plot.GetValues(), abstract_plot.GetLabels());
            return plot_data;
        }

        public void Test()
        {
            OnModelChange(EventArgs.Empty);
        }

        private Activity firstAct()
        {
            Activity first_act = activities[0];
            foreach (Activity act in activities)
            {
                if (act.start_date < first_act.start_date)
                {
                    first_act = act;
                }
            }
            return first_act;
        }

        private Activity lastAct()
        {
            Activity last_act = activities[activities.Count - 1];
            foreach (Activity act in activities)
            {
                if (act.start_date > last_act.start_date)
                {
                    last_act = act;
                }
            }
            return last_act;
        }



        protected virtual void OnActivitiesSet(EventArgs e)
        {
            ActivitiesSet?.Invoke(this, e);
        }

        protected virtual void OnModelChange(EventArgs e)
        {
            ModelChanged?.Invoke(this, e);
        }

        

    }

        
}
