namespace StravaViewer.Models
{
    public class PlotData
    {
        public double[] Values;
        public double[] Positions;
        public string[] Labels;
        public bool isDetailPlot;

        public PlotData(double[] values, string[] labels)
        {
            this.Values = values;
            this.Labels = labels;

            List<double> positions_list = new List<double>();
            for (int i=0; i < values.Length; i++)
            {
                positions_list.Add(i);
            }

            this.Positions = positions_list.ToArray();
        }

        public static PlotData Empty()
        {
            double[] values = new double[0];
            string[] labels = new string[0];
            PlotData data = new PlotData(values, labels);
            return data;
        }
    }
}
