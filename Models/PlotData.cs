namespace StravaViewer.Models
{
    public class PlotData
    {
        public List<double[]> Values = new List<double[]>();
        public double[] Positions;
        public string[] Labels;
        public bool isDetailPlot;

        public PlotData(double[] values, string[] labels)
        {
            this.Values.Add(values);
            this.Labels = labels;

            List<double> positions_list = new List<double>();
            for (int i=0; i < values.Length; i++)
            {
                positions_list.Add(i);
            }

            this.Positions = positions_list.ToArray();
        }

        public PlotData(AbstractPlot.AbstractSummaryPlot plot)
        {
            this.Values.Add(plot.GetValues());
            this.Labels = plot.GetLabels();

            List<double> positions_list = new List<double>();
            for (int i = 0; i < Values[0].Length; i++)
            {
                positions_list.Add(i);
            }

            this.Positions = positions_list.ToArray();
        }

        public PlotData(AbstractPlot.AbstractDetailPlot plot)
        {
            this.Values = plot.GetValues();
            this.Labels = plot.GetLabels();

            List<double> positions_list = new List<double>();
            for (int i = 0; i < Values[0].Length; i++)
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
