using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P04WeatherForecastAPI.Client.Models
{
    public class Alarm
    {
        public string AlarmType { get; set; }
        public AlarmVal Value { get; set; }
        public Day Day { get; set; }
        public Night Night { get; set; }
    }
}
