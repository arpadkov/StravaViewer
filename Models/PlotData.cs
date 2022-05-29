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
            this.Values = ConvertValues(plot.GetValues());
            this.Labels = plot.GetLabels();

            List<double> positions_list = new List<double>();
            for (int i = 0; i < Values[0].Length; i++)
            {
                positions_list.Add(i);
            }

            this.Positions = positions_list.ToArray();
        }

        /*
         * converts the values:
         * to simulate stacking, shifts values
         * TERRIBLE
         */
        private List<double[]> ConvertValues(List<double[]> valueSeries)
        {
            List<double[]> result = new List<double[]>();

            double[] values_offset = new double[valueSeries[0].Length];
            for (int i = 0; i < valueSeries.Count; i++)            
            {
                
                double[] new_values = offsetSeries(valueSeries[i], values_offset);

                values_offset = new_values;

                result.Add(new_values);
            }

            return Enumerable.Reverse(result).ToList();
        }

        private double[] offsetSeries(double[] original, double[] offset)
        {
            double[] result = new double[original.Length];
            for (int i = 0; i < original.Length; i++)
            {
                result[i] = original[i] + offset[i];
            }
            return result;
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
