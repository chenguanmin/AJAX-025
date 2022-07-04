using AJAX_025.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AJAX_025.Controllers
{
    public class ApiController : Controller
    {
        private readonly DemoContext _demo;

        public ApiController(DemoContext demo)
        {
            _demo = demo;
        }

        public IActionResult Index(User user)
        {
            //System.Threading.Thread.Sleep(3000); //停止5秒鐘

            if (string.IsNullOrEmpty(user.name))
            {
                user.name = "Ajax";
            }
            return Content($"{user.name}你好 你的年紀是{user.age}", "text/html", System.Text.Encoding.UTF8);

        }

        public IActionResult FirstAjax()
        {
            return View();
        }

        public IActionResult Ajaxpost()
        {
            return View();
        }

        public IActionResult homework2()
        {

            return View();
        }
        public bool hasexit(string name)
        {
            var result = from m in _demo.Members
                         where m.Name == name
                         select m.Name;
            if (result.Count() > 0)
            {
                return true;
            }
            return false;
        }




    }
}
