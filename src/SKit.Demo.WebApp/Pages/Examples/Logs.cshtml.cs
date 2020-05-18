using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Localization;
using SKit.Demo.WebApp.Services;

using SKit.Demo.WebApp.Models;

// https://mdbootstrap.com/education/bootstrap/quick-start/
// https://fontawesome.com/icons?d=gallery&m=free
// https://mdbootstrap.com/docs/jquery/tables/basic/
// https://mdbootstrap.com/docs/jquery/tables/datatables/

namespace SKit.Demo.WebApp.Pages.Examples
{
    public class LogsModel : PageModel
    {
        public class InputModel
        {
            [Display(Name = "Starting from")]
            [RegularExpression(@"^(2[0-3]|[01]?[0-9]):([0-5]?[0-9])$", ErrorMessage = "The time should be in the format 'HH:MM'")]
            [Required]
            public string FromTime { get; set; } = "07:00";

            //[Display(Name = "Maximum events")]
            //public int MaxEvents { get; set; } = 10000;
        }

        private static int MAX_EVENTS = 10000;
        private readonly IDailyLogService _dailyLogService;

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        public DailyLogBlock Data { get; set; }

        public string OriginalLogFileURL { get; set; }

        public LogsModel(IDailyLogService dailyLogService)
        {
            _dailyLogService = dailyLogService;
            OriginalLogFileURL = _dailyLogService.GetLogURL(DateTime.Now);
        }

        public async void OnGet()
        {
            Data = await _dailyLogService.Get(FromTimeToDateTime(Input.FromTime), MAX_EVENTS);
            Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                Data = await _dailyLogService.Get(FromTimeToDateTime(Input.FromTime), MAX_EVENTS);
                return Page();
            }
            return RedirectToPage();
        }

        public DateTime FromTimeToDateTime(string time)
        {
            var timeSpan = TimeSpan.Parse(time);
            var fromStr = $"{DateTime.Now:yyyy-MM-dd} {time}";
            var from = DateTime.Now.Date + timeSpan;
            return from;
        }
    }
}
