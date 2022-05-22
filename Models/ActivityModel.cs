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
        YearlySummary,      // displayTime - all,       grouped by - years
        MonthlySummary,     // displayTime - year,      grouped by - month
        MonthDetail,        // displayTime - month,     individual activities displayed
        UniqueDetail,       // displayTime - unique,    individual activities displayed
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
            this.Client = new StravaClient("95.arpadkov");
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
            else if (plotType == PlotType.MonthDetail)
            {
                return getMonthDetailPlot();
            }

            else
            {
                return PlotData.Empty();
            }
        }

        private PlotData getMonthlySummaryPlot()
        {
            displayTime = TimePeriod.FromYear(displayTime.EndYear);

            var abstract_plot = new AbstractMonthlySummaryPlot(getActivitiesByType(), displayTime);

            var plot_data = new PlotData(abstract_plot.GetValues(), abstract_plot.GetLabels());
            return plot_data;
        }

        private PlotData getYearlySummaryPlot()
        {
            displayTime = new TimePeriod(firstAct().start_date, lastAct().start_date);

            var abstract_plot = new AbstractYearlySummaryPlot(getActivitiesByType(), displayTime);

            var plot_data = new PlotData(abstract_plot.GetValues(), abstract_plot.GetLabels());
            return plot_data;
        }

        private PlotData getMonthDetailPlot()
        {
            var plot_data = PlotData.Empty();

            return plot_data;
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

        /* TODO Misi
         * this method inkrements the current display time by 1 unit
         * - determine the unit of the current displayTime (all, year, month) from plotType
         * - set displayTime for the next timeperiod
         * - invoke ModelChanged event
         */
        public void NextDisplayTime()
        {
            // Misi started
        }

        /* TODO Misi
         * see NextDisplayTime
         */
        public void LastDisplayTime()
        {

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
