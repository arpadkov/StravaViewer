using System.Data;
using Newtonsoft.Json.Linq;

namespace StravaViewer.Client.Activity
{
    public class ActivityLaps
    {
        public DataTable LapsTable;
        List<ActivityLap> laps = new List<ActivityLap>();

        public ActivityLaps(JArray JLaps)
        {
            LapsTable = new DataTable("ActivityTable");
            DataRow row;

            LapsTable.Columns.Add(new DataColumn("Index", Type.GetType("System.Int32")));
            LapsTable.Columns.Add(new DataColumn("Distance", Type.GetType("System.Single")));
            LapsTable.Columns.Add(new DataColumn("Average Pace", Type.GetType("System.String")));
            LapsTable.Columns.Add(new DataColumn("Time", Type.GetType("System.String")));
            LapsTable.Columns.Add(new DataColumn("Average Heartrate", Type.GetType("System.Single")));

            foreach (var Jlap in JLaps)
            {
                var lap = new ActivityLap(Jlap.ToObject<JObject>());
                laps.Add(lap);

                row = LapsTable.NewRow();
                row["Index"] = lap.index;
                row["Distance"] = lap.distance/1000;
                row["Average Pace"] = lap.average_pace.ToString(@"mm\:ss");
                row["Time"] = lap.time.ToString(@"mm\:ss");
                row["Average Heartrate"] = lap.average_heartrate;
                LapsTable.Rows.Add(row);
            }
        }

        public ActivityLap GetActivityLap(int index)
        {
            return laps[index];
        }
    }
}
