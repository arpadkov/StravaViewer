using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StravaViewer.Models
{
    public class TimePeriod
    {
        private DateTime _startTime;
        private DateTime _endTime;

        public TimePeriod(DateTime startTime, DateTime endTime)
        {
            this._startTime = startTime;
            this._endTime = endTime;
        }

        public DateTime StartTime { get { return _startTime; } }
        public DateTime EndTime { get { return _endTime; } }

        public static TimePeriod FromYear(int year)
        {
            DateTime firstDay = new DateTime(year, 1, 1);
            DateTime lastDay = new DateTime(year+1, 1, 1);

            return new TimePeriod(firstDay, lastDay);
        }

        public string ToString()
        {
            return _startTime.ToString("yyyy-MMM-dd") + " - " + _endTime.ToString("yyyy-MMM-dd");
        }


    }



}
