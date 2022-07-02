using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace StravaViewer.Client.Activity
{
    public class ActivityStreams
    {
        public List<double> distances = new List<double>();
        public double[] distances_lowres;
        public List<double> times = new List<double>();
        public double[] times_lowres;
        public List<double> elevations = new List<double>();
        public double[] elevations_lowres;
        public List<double> heartrates = new List<double>();
        public double[] heartrates_lowres;
        public List<double> velocities = new List<double>();
        public double[] velocities_lowres;

        public List<float[]> latlngs = new List<float[]>();
        

        public ActivityStreams(Dictionary<string, JArray> JStreams, int resolution)
        {
            int length = JStreams["time"].Count;

            // Initializing Lists for lowres arrays
            List<double> distances_list_lowres = new List<double>();
            List<double> times_list_lowres = new List<double>();
            List<double> elevations_list_lowres = new List<double>();
            List<double> heartrates_list_lowres = new List<double>();
            List<double> velocities_list_lowres = new List<double>();

            // Filling up highres Lists
            for (int i = 0; i < length; i ++)
            {
                distances.Add((double)JStreams["distance"][i] / 1000);
                times.Add((double)JStreams["time"][i]);
                elevations.Add((double)JStreams["elevation"][i]);
                heartrates.Add((double)JStreams["heartrate"][i]);
                velocities.Add((double)JStreams["heartrate"][i]);

                // Filling up LatLng List
                float[] coords = new float[2];
                coords[0] = (float)JStreams["latlng"][i][0];
                coords[1] = (float)JStreams["latlng"][i][1];
                latlngs.Add(coords);
            }

            // Filling up lowres Lists
            for (int i = 0; i < resolution; i++)
            {
                int index = (int)Math.Round(i * ((float)length / resolution));

                distances_list_lowres.Add((double)JStreams["distance"][index] / 1000);
                times_list_lowres.Add((double)JStreams["time"][index]);
                elevations_list_lowres.Add((double)JStreams["elevation"][index]);
                heartrates_list_lowres.Add((double)JStreams["heartrate"][index]);
                velocities_list_lowres.Add((double)JStreams["heartrate"][index]);
            }

            // Converting lowres lists to arrays
            distances_lowres = distances_list_lowres.ToArray();
            times_lowres = times_list_lowres.ToArray();
            elevations_lowres = elevations_list_lowres.ToArray();
            heartrates_lowres = heartrates_list_lowres.ToArray();
            velocities_lowres = velocities_list_lowres.ToArray();
        }
    }
}
