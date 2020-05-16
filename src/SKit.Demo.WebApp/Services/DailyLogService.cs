using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SKit.Demo.WebApp.Models;

namespace SKit.Demo.WebApp.Services
{
    public class DailyLogService : IDailyLogService
    {
        public DailyLogBlock Get(DateTime? startingFrom = null, int maxEvents = 100)
        {
            var eventList = new List<LogEventItem>();
            var result = new DailyLogBlock()
            {
                StartingFrom = startingFrom ?? DateTime.Now,
                Events = eventList
            };

            var filePath = GetLogPath(result.StartingFrom);
            if (!File.Exists(filePath))
                return result;

            var lines = File.ReadAllLines(filePath);
            
            var count = 0;
            foreach (var line in lines)
            {
                var item = LineParse(line);
                if (item == null || item.EventTime < startingFrom)
                    continue;
                eventList.Add(item);

                if (++count >= maxEvents)
                    break;
            }

            return result;
        }

        private string GetLogPath(DateTime logDate)
        {
            var fileName = $"skit.demo.webapp_{logDate:yyyyMMdd}.log";
            var result = Path.Combine(Environment.CurrentDirectory, "Files", "Logs", fileName);
            return result;
        }

        private LogEventItem LineParse(string line)
        {
            LogEventItem result = null;
            if (string.IsNullOrWhiteSpace(line) || line.Length < 36)
                return result;
            
            var dateTimeString = line.Substring(0, 30);
            if (!DateTime.TryParse(dateTimeString, out var dateTime))
                return result;

            var typeAndMsg = line.Remove(0, 31);
            var eventType = "";
            var bracketBegIndex = typeAndMsg.IndexOf('[');
            var msg = typeAndMsg;
            if (bracketBegIndex >= 0)
            {
                var bracketEndIndex = typeAndMsg.IndexOf(']');
                if (bracketEndIndex >= 0)
                {
                    eventType = typeAndMsg.Substring(bracketBegIndex + 1, bracketEndIndex - bracketBegIndex - 1);
                    msg = typeAndMsg.Substring(bracketEndIndex + 2);
                }
             }

            result = new LogEventItem()
            {
                EventTime = dateTime,
                EventType = eventType,
                Message = msg
            };
            return result;
        }
    }
}
