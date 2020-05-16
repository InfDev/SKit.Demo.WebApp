using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SKit.Demo.WebApp.Models;

namespace SKit.Demo.WebApp.Services
{
    public interface IDailyLogService
    {
        Task<DailyLogBlock> Get(DateTime? startingFrom = null, int maxEvents = 100);
    }
}
