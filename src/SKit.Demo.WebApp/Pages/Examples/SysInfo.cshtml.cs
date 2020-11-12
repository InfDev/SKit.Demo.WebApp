using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using SKit.Common.Models;
using SKit.Common.Helpers;
using Microsoft.Extensions.Configuration;

namespace SKit.Demo.WebApp.Pages.Examples
{
    public class SysInfoModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public SysInfoModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<ValueText> Infos { get; set; }
        
        public void OnGet()
        {
            Infos = SystemHelper.GetSystemInfo(_configuration);
        }
    }
}