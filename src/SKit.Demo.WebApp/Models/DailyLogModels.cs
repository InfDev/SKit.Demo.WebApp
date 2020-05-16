using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKit.Demo.WebApp.Models
{
    public class LogEventItem
    {
        public DateTime EventTime{ get; set; }
        public string EventType { get; set; }
        public string Message { get; set; }
    }

    public class DailyLogBlock
    {
        public DateTime StartingFrom { get; set; }
        public IReadOnlyList<LogEventItem> Events { get; set; }
    }

}
