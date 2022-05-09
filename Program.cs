using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StravaViewer.Client;
using StravaViewer.Models;

namespace StravaViewer
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            MainWindow mainWindow = new MainWindow();

StravaClient Client = new StravaClient();
            Client.SetAccesToken("95.arpadkov");

            JArray json_activities = Client.GetAllActivities();
            List<Activity> activities = new List<Activity>();

            foreach (JObject json_activity in json_activities)
            {
                activities.Add(new Activity(json_activity));
            }

            mainWindow.SetLabel(activities.Count.ToString());


            Application.Run(mainWindow);

            

        }
    }
}