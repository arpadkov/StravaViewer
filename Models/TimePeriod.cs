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

        public TimePeriod(DateTime _startTime, DateTime _endTime)
        {
            this._startTime = _startTime;
            this._endTime = _endTime;
        }

        public DateTime StartTime { get { return _startTime; } }
        public DateTime EndTime { get { return _endTime; } }

        public int StartYear { get { return _startTime.Year; } }
        public int EndYear { get { return _endTime.Year; } }

        public static TimePeriod FromYear(int year)
        {
            DateTime firstDay = new DateTime(year, 1, 1);
            DateTime lastDay = new DateTime(year+1, 1, 1);

            return new TimePeriod(firstDay, lastDay);
        }


    }



}
