using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Localization;

namespace SKit.Demo.WebApp.Pages.Examples
{
    public class LogsModel : PageModel
    {
        public class InputLogModel
        {
            [Display(Name = "Protocol Date")]
            public DateTime LogDate { get; set; } = DateTime.Now.Date;


            [Display(Name = "Starting from")]
            public DateTime? StartingFrom { get; set; }

            [Display(Name = "Maximum number of events")]
            [Range(0, 100)]
            public int MaxEvents { get; set; } = 50;
        }


        public void OnGet()
        {

        }


    }
}