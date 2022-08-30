using Newtonsoft.Json.Linq;
using System.Xml;

namespace StravaViewer.Client.Activity
{
    public class ActivityRoute
    {
        public List<float[]> latlngs = new List<float[]>();
        public List<float[]> latlngs_lowres = new List<float[]>();

        int highres_resolution;
        int lowres_resolution;

        public ActivityRoute(JArray points, int resolution = 500)
        {
            highres_resolution = points.Count;
            lowres_resolution = resolution;

            // filling up highres list
            for (int i = 0; i < highres_resolution; i++)
            {
                float[] coords = new float[2];
                coords[0] = (float)points[i][0];
                coords[1] = (float)points[i][1];
                latlngs.Add(coords);
            }

            // filling up lowres list
            for (int i = 0; i < lowres_resolution; i++)
            {
                int index = (int)Math.Round(i * ((float)highres_resolution / lowres_resolution));

                // Filling up LatLng List
                float[] coords = new float[2];
                coords[0] = (float)points[index][0];
                coords[1] = (float)points[index][1];
                latlngs_lowres.Add(coords);
            }
        }

        public ActivityRoute(string raw_gpx, int resolution = 500)
        {

            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(raw_gpx);

            XmlNodeList xmlpoints;
            xmlpoints = xmldoc.GetElementsByTagName("trkpt");

            highres_resolution = xmlpoints.Count;
            lowres_resolution = resolution;

            var number_style = System.Globalization.CultureInfo.InvariantCulture;

            // filling up highres list
            for (int i = 0; i < highres_resolution; i++)
            {
                float[] coords = new float[2];
                coords[0] = float.Parse(xmlpoints[i].Attributes["lat"].Value, number_style);
                coords[1] = float.Parse(xmlpoints[i].Attributes["lon"].Value, number_style);
                latlngs.Add(coords);
            }

            // filling up lowres list
            for (int i = 0; i < lowres_resolution; i++)
            {
                int index = (int)Math.Round(i * ((float)highres_resolution / lowres_resolution));

                // Filling up LatLng List
                float[] coords = new float[2];
                coords[0] = float.Parse(xmlpoints[index].Attributes["lat"].Value, number_style);
                coords[1] = float.Parse(xmlpoints[index].Attributes["lon"].Value, number_style);
                latlngs_lowres.Add(coords);
            }
        }

        // TODO
        public float[] FirstPoint()
        {
            return new float[0];
        }

        // TODO
        public float[] LastPoint()
        {
            return new float[0];
        }
    }
}
