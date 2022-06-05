namespace StravaViewer.Models.AbstractPlot
{
    public class PlotData
    {
        public List<double[]> valueSeries = new List<double[]>();
        public double[] Positions;
        public string[] Labels;
        public bool IsEmpty;

        public PlotData(List<double[]> values, string[] labels, bool IsEmpty = false)
        {
            this.valueSeries = values;
            this.Labels = labels;
            this.IsEmpty = IsEmpty;

            List<double> positions_list = new List<double>();
            for (int i = 0; i < valueSeries[0].Length; i++)
            {
                positions_list.Add(i);
            }

            Positions = positions_list.ToArray();
        }

        public PlotData(AbstractPlot plot)
        {
            valueSeries = plot.GetValues();
            Labels = plot.GetLabels();

            List<double> positions_list = new List<double>();
            for (int i = 0; i < valueSeries[0].Length; i++)
            {
                positions_list.Add(i);
            }

            Positions = positions_list.ToArray();
        }

        //public PlotData(AbstractPlot plot)
        //{
        //    Values = ConvertValues(plot.GetValues());
        //    Labels = plot.GetLabels();

        //    List<double> positions_list = new List<double>();
        //    for (int i = 0; i < Values[0].Length; i++)
        //    {
        //        positions_list.Add(i);
        //    }

        //    Positions = positions_list.ToArray();
        //}

        ///*
        // * converts the values:
        // * to simulate stacking, shifts values
        // * TERRIBLE
        // */
        //private List<double[]> ConvertValues(List<double[]> valueSeries)
        //{
        //    List<double[]> result = new List<double[]>();

        //    double[] values_offset = new double[valueSeries[0].Length];
        //    for (int i = 0; i < valueSeries.Count; i++)
        //    {

        //        double[] new_values = offsetSeries(valueSeries[i], values_offset);

        //        values_offset = new_values;

        //        result.Add(new_values);
        //    }

        //    return Enumerable.Reverse(result).ToList();
        //}

        //private double[] offsetSeries(double[] original, double[] offset)
        //{
        //    double[] result = new double[original.Length];
        //    for (int i = 0; i < original.Length; i++)
        //    {
        //        result[i] = original[i] + offset[i];
        //    }
        //    return result;
        //}

        public static PlotData Empty()
        {
            List<double[]> valueSeries = new List<double[]>();
            valueSeries.Add(new double[0]);
            string[] labels = new string[0];
            PlotData data = new PlotData(valueSeries, labels, IsEmpty: true);
            return data;
        }
    }
}
