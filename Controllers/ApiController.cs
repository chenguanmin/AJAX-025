using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AJAX_025.Controllers
{
    public class ApiController : Controller
    {
        public IActionResult Index()
        {
            return Content("<h2>hello ajax</h2>","text/html", System.Text.Encoding.UTF8);
        }
    }
}
