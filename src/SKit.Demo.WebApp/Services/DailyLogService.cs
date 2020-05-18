using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SKit.Demo.WebApp.Models;
using System.Text;

namespace SKit.Demo.WebApp.Services
{
    public class DailyLogService : IDailyLogService
    {
        public Task<DailyLogBlock> Get(DateTime? startingFrom = null, int maxEvents = 100)
        {
            return Task<DailyLogBlock>.Factory.StartNew(() =>
            {
                var eventList = new List<LogEventItem>();
                var result = new DailyLogBlock()
                {
                    StartingFrom = startingFrom ?? DateTime.Now.Date,
                    Events = eventList
                };

                var filePath = GetLogPath(result.StartingFrom);
                if (!File.Exists(filePath))
                    return result;

                var lines = InternalReadAllLines(filePath, Encoding.UTF8);

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
            });
        }

        private string GetLogFileName(DateTime logDate)
        {
            return $"skit.demo.webapp_{logDate:yyyyMMdd}.log";
        }

        private string GetLogPath(DateTime logDate)
        {
            return Path.Combine(Environment.CurrentDirectory, 
                "Files", "Logs", GetLogFileName(logDate));
        }

        public string GetLogURL(DateTime logDate)
        {
            return $"/Files/Logs/{GetLogFileName(logDate)}";
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

        private string[] InternalReadAllLines(string path, Encoding encoding)
        {
            List<string> list = new List<string>();

            using (var file = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader streamReader = new StreamReader(file, encoding))
                {
                    string str;
                    while ((str = streamReader.ReadLine()) != null)
                        list.Add(str);
                }
            }
            return list.ToArray();
        }
    }
}
