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

namespace SKit.Demo.WebApp.Pages.Examples
{
    public class LogsModel : PageModel
    {
        public class InputModel
        {
            [Display(Name = "Starting from")]
            public DateTime? StartingFrom { get; set; }

            [Display(Name = "Maximum events")]
            [Range(0, 100)]
            public int MaxEvents { get; set; } = 50;
        }

        private readonly IDailyLogService _dailyLogService;

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        public DailyLogBlock Data { get; set; }

        public LogsModel(IDailyLogService dailyLogService)
        {
            _dailyLogService = dailyLogService;
        }

        public async void OnGet()
        {
            Data = await _dailyLogService.Get(Input.StartingFrom, Input.MaxEvents);
            Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Data = await _dailyLogService.Get(Input.StartingFrom, Input.MaxEvents);
            return Page();
        }
    }
}
