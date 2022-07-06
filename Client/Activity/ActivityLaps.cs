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
            LapsTable.Columns.Add(new DataColumn("Average Pace", Type.GetType("System.Single")));
            LapsTable.Columns.Add(new DataColumn("Time", Type.GetType("System.Single")));
            LapsTable.Columns.Add(new DataColumn("Average Heartrate", Type.GetType("System.Single")));

            foreach (var Jlap in JLaps)
            {
                var lap = new ActivityLap(Jlap.ToObject<JObject>());
                laps.Add(lap);

                row = LapsTable.NewRow();
                row["Index"] = Jlap["lap_index"].ToObject<int>();
                row["Distance"] = Jlap["distance"].ToObject<float>();
                row["Average Pace"] = Jlap["average_speed"].ToObject<float>();
                row["Time"] = Jlap["elapsed_time"].ToObject<float>();
                row["Average Heartrate"] = Jlap["average_heartrate"].ToObject<float>();
                LapsTable.Rows.Add(row);
            }
        }

        public ActivityLap GetActivityLap(int index)
        {
            return laps[index];
        }
    }
}
