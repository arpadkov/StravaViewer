using Newtonsoft.Json.Linq;
using StravaViewer.Client;
using StravaViewer.Models.AbstractPlot;

namespace StravaViewer.Models
{
    public enum InfoType
    {
        Distance,
        ElevationGain,
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

        public AbstractPlot.AbstractPlot? AbstractPlot; //temporary

        public ActivityModel()
        {
            
            //this.displayTime = new TimePeriod(DateTime.Now, DateTime.Now);
            //this.displayTime = TimePeriod.FromYear(2019);

            this.activityType = ActivityType.Run;
            this.infoType = InfoType.Distance;
            this.plotType = PlotType.YearlySummary;

            this.activities = new List<Activity>();
            this.Client = new StravaClient("95.arpadkov");

            SetActivities();
            this.displayTime = InitializeDisplaytime();
            //this.displayTime = new TimePeriod(new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1), new DateTime(DateTime.Today.Year, DateTime.Today.Month + 1, 1));
            //displayTime = new TimePeriod(new DateTime(2022, 05, 01), new DateTime(2022, 06, 01));
            //displayTime = new TimePeriod(new DateTime(2021, 12, 01), new DateTime(2022, 01, 01));
            SetAbstractPlot();
        }

        public TimePeriod InitializeDisplaytime()
        {
            TimePeriod timePeriod;
            if (PlotType == PlotType.MonthlySummary)
            {
                timePeriod = TimePeriod.FromYear(DateTime.Today.Year);
            }
            else if (PlotType == PlotType.MonthDetail)
            {
                timePeriod = new TimePeriod(new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1), new DateTime(DateTime.Today.Year, DateTime.Today.Month+1, 1));
            }
            else
            {
                timePeriod = new TimePeriod(firstAct().start_date, lastAct().start_date);
            }
            return timePeriod;
        }

        public TimePeriod DisplayTime
        {
            get { return this.displayTime; }
            set
            {
                this.displayTime = value;
                SetAbstractPlot();
                OnModelChange(EventArgs.Empty);
            }
        }

        public PlotType PlotType
        {
            get { return this.plotType; }
            set
            {
                this.plotType = value;
                SetAbstractPlot();
                OnModelChange(EventArgs.Empty);
            }
        }

        public InfoType InfoType
        {
            get { return this.infoType; }
            set
            {
                this.infoType = value;
                SetAbstractPlot();
                OnModelChange(EventArgs.Empty);
            }
        }

        public ActivityType ActivityType
        {
            get { return this.activityType; }
            set
            {
                this.activityType = value;
                SetAbstractPlot();
                OnModelChange(EventArgs.Empty);
            }
        }

        public List<ActivityCollection> ActivityCollections
        {
            get { return AbstractPlot.activityCollections; }
            set { }
        }

        public List<BoundingRectangle> BoundingRectangles
        {
            get 
            {
                List<BoundingRectangle> result = new List<BoundingRectangle>();
                for (int i = 0; i < AbstractPlot.activityCollections.Count; i++)
                {
                    result.Add(AbstractPlot.activityCollections[i].BoundingRectangle);
                }
                return result;
            }
            set { }
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

        public void SetAbstractPlot()
        {
            if (plotType == PlotType.YearlySummary)
            {
                this.AbstractPlot = SetYearlySummaryPlot();
            }
            else if (plotType == PlotType.MonthlySummary)
            {
                this.AbstractPlot = SetMonthlySummaryPlot();
            }
            else if (plotType == PlotType.MonthDetail)
            {
                this.AbstractPlot = SetMonthDetailPlot();
            }
        }

        private AbstractPlot.AbstractPlot SetMonthlySummaryPlot()
        {
            //displayTime = TimePeriod.FromYear(2022);

            var abstract_plot = new AbstractMonthlySummaryPlot(getActivitiesByType(), displayTime, InfoType);

            //var plot_data = new PlotData(abstract_plot.GetValues(), abstract_plot.GetLabels());
            //this.AbstractPlot = abstract_plot; //temporary
            return abstract_plot;
        }

        private AbstractPlot.AbstractPlot SetYearlySummaryPlot()
        {
            

            var abstract_plot = new AbstractYearlySummaryPlot(getActivitiesByType(), displayTime, InfoType);

            //var plot_data = new PlotData(abstract_plot.GetValues(), abstract_plot.GetLabels());
            //this.AbstractPlot = abstract_plot; //temporary
            return abstract_plot;
        }

        private AbstractDetailPlot SetMonthDetailPlot()
        {
            ///////////////////////!!!!!!!!!!!!!!!!!!!!!!!!!!!!!/////////////////////////////////
            //displayTime = new TimePeriod(new DateTime(2022, 06, 01), new DateTime(2022, 07, 01));

            var abstract_plot = new AbstractDetailPlot(getActivitiesByType(), displayTime, InfoType);

            return abstract_plot;
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
         */
        public void NextDisplayTime()
        {
            if (PlotType == PlotType.MonthlySummary)
            {
                DisplayTime = new TimePeriod(DisplayTime.StartTime.AddYears(1), DisplayTime.EndTime.AddYears(1));
            }
            else if (PlotType == PlotType.MonthDetail)
            {
                DisplayTime = new TimePeriod(DisplayTime.StartTime.AddMonths(1), DisplayTime.EndTime.AddMonths(1));
            }
        }

        /* TODO Misi
         * see NextDisplayTime
         */
        public void LastDisplayTime()
        {
            if (PlotType == PlotType.MonthlySummary)
            {
                DisplayTime = new TimePeriod(DisplayTime.StartTime.AddYears(-1), DisplayTime.EndTime.AddYears(-1));
            }
            else if (PlotType == PlotType.MonthDetail)
            {
                DisplayTime = new TimePeriod(DisplayTime.StartTime.AddMonths(-1), DisplayTime.EndTime.AddMonths(-1));
            }
        }

        public void DrillDown(ActivityCollection activityCollection)
        {
            if (PlotType == PlotType.YearlySummary)
            {
                int year = activityCollection.activities[0].start_date.Year;
                PlotType = PlotType.MonthlySummary;
                DisplayTime = new TimePeriod(new DateTime(year, 01, 01), new DateTime(year + 1, 01, 01));
            }

            else if (PlotType == PlotType.MonthlySummary)
            {
                int year = activityCollection.activities[0].start_date.Year;
                int month = activityCollection.activities[0].start_date.Month;
                PlotType = PlotType.MonthDetail;
                DisplayTime = new TimePeriod(new DateTime(year, month, 01), new DateTime(year, month + 1, 01));
            }
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
