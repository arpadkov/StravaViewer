using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace StravaViewer.Client.Activity
{
    public class ActivityStreams
    {
        //public List<double>? distances = new List<double>();
        //public double[]? distances_lowres;
        //public List<double>? times = new List<double>();
        //public double[]? times_lowres;
        //public List<double>? elevations = new List<double>();
        //public double[]? elevations_lowres;
        //public List<double>? heartrates = new List<double>();
        //public double[]? heartrates_lowres;
        //public List<double>? velocities = new List<double>();
        //public double[]? velocities_lowres;
        //private List<float[]>? latlngs = new List<float[]>();
        //private List<float[]>? latlngs_lowres = new List<float[]>();

        public ActivityRoute Route;

        Dictionary<string, List<double>> highres_streams;
        Dictionary<string, double[]> lowres_streams;

        int highres_resolution;
        int lowres_resolution;



        public ActivityStreams(Dictionary<string, JArray> JStreams, int resolution = 500)
        {
            Route = new ActivityRoute(JStreams["latlng"]);

            highres_resolution = JStreams["time"].Count;
            lowres_resolution = resolution;

            highres_streams = new Dictionary<string, List<double>>
            {
                {"distance", new List<double>()},
                {"time", new List<double>()},
                {"altitude", new List<double>()},
                {"heartrate", new List<double>()},
                {"velocity_smooth", new List<double>()},
            };

            // Filling up highres Lists
            foreach (var item in JStreams)
            {
                if (item.Key != "latlng")
                {
                    FillHighresList(item.Key, item.Value);
                }
            }

            lowres_streams = new Dictionary<string, double[]>
            {
                {"distance", new double[0]},
                {"time", new double[0]},
                {"altitude", new double[0]},
                {"heartrate", new double[0]},
                {"velocity_smooth", new double[0]},
            };

            // Filling up lowres Lists
            foreach (var item in JStreams)
            {
                if (item.Key != "latlng")
                {
                    FillLowresList(item.Key, item.Value);
                }
            }

            SetUnits();
        }

        public double[] GetLowresStream(string stream)
        {
            if (lowres_streams[stream].Length == 0)
            {
                throw new NotImplementedException();
            }

            return lowres_streams[stream];
        }

        public List<double> GetStream(string stream)
        {
            return highres_streams[stream];
        }

        private void FillHighresList(string stream, JArray values)
        {
            for (int i = 0; i < highres_resolution; i++)
            {
                highres_streams[stream].Add((double)values[i]);
            }
        }

        private void FillLowresList(string stream, JArray values)
        {
            // temporary list, used to convert to array
            List<double> list_lowres = new List<double>();

            for (int i = 0; i < lowres_resolution; i++)
            {
                int index = (int)Math.Round(i * ((float)highres_resolution / lowres_resolution));

                list_lowres.Add((double)values[index]);
            }

            //var x = lowres_mapping[stream];

            //x = list_lowres.ToArray();

            lowres_streams[stream] = list_lowres.ToArray();

        }

        private void SetUnits()
        {
            // Changing the distance values from m to km
            for (int i = 0; i < highres_resolution; i++)
            {
                highres_streams["distance"][i] = highres_streams["distance"][i] / 1000;
            }

            for (int i = 0; i < lowres_resolution; i++)
            {
                lowres_streams["distance"][i] = lowres_streams["distance"][i] / 1000;
            }
        }

        public int IndexOfClosestDistance(double distance)
        {
            double closest = GetLowresStream("distance")[0];
            double minDifference = Math.Abs(GetLowresStream("distance")[0] - distance);

            foreach (double val in GetLowresStream("distance"))
            {
                double dif = Math.Abs(distance - val);

                if (dif < minDifference)
                {
                    minDifference = dif;
                    closest = val;
                }
            }

            int index = Array.IndexOf(GetLowresStream("distance"), closest);
            return index;
        }

        public double DistanceFromLatLng(double lat, double lng, float acceptable_radius)
        {
            // returns the closest distance of the activity, if the given point is inside the acceptable_radius
            // return -1 if the given point is not inside the radius of any activity points

            // distance to the first point
            float min_distance = (float)distance((float)lat, (float)lng, Route.latlngs_lowres[0][0], Route.latlngs_lowres[0][1]);
            float current_distance;
            int index = 0;
            int min_index = 0;
            foreach (float[] point in Route.latlngs_lowres)
            {
                current_distance = (float)distance((float)lat, (float)lng, point[0], point[1]);
                if (current_distance < min_distance)
                {
                    min_distance = current_distance;
                    min_index = index;
                }
                index++;
            }

            if (min_distance > acceptable_radius)
            {
                return -1;
            }
            else
            {
                return GetLowresStream("distance")[min_index];
            }

        }

        private double distance(float lat1, float lon1, float lat2, float lon2)
        {
            // should calculate the distance between 2 lat-lng points.. maybe

            var p = 0.017453292519943295;    // Math.PI / 180
            var c = Math.Cos;
            var a = 0.5 - c((lat2 - lat1) * p) / 2 +
                    c(lat1 * p) * c(lat2 * p) *
                    (1 - c((lon2 - lon1) * p)) / 2;

            return 12742 * Math.Asin(Math.Sqrt(a)); // 2 * R; R = 6371 km
        }
    }
}
