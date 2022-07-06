using AJAX_025.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AJAX_025.Controllers
{
    public class ApiController : Controller
    {
        private readonly DemoContext _demo;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ApiController(DemoContext demo,IWebHostEnvironment hostingEnvironment)
        {
            _demo = demo;
            _hostingEnvironment = hostingEnvironment;
        }     

        public IActionResult Index(Member user, IFormFile file)
        {
            //System.Threading.Thread.Sleep(3000); //停止5秒鐘
            string resultFile = $"{file.FileName} - {file.Length} - {file.ContentType}";           

            //檔案上傳要有實際路徑
            //C:\Users\Student\Documents\Ajax\MSIT141Site\wwwroot\uploads
            //string path = _host.ContentRootPath; //會取得專案資料夾的實際路徑

            string imgName = Guid.NewGuid().ToString() + file.FileName;

            //會取得專案資料夾下wwwroot的實際路徑
            string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images", imgName);
            
            //string filePath = Path.Combine(uploadsFolder, imgName);
            using (var fileStream = new FileStream(uploadsFolder, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
            byte[] imgbyte;
            using (var memory = new MemoryStream())
            {
                file.CopyTo(memory);
                imgbyte = memory.ToArray();
            }
            user.FileName = imgName;
            user.FileData = imgbyte;

            _demo.Members.Add(user);
            _demo.SaveChanges();

            if (string.IsNullOrEmpty(user.Name))
            {
                user.Name = "Ajax";
            }
            return Content($"{user.Name}你好 你的年紀是{user.Age} 相片上傳成功: {resultFile}", "text/plain", System.Text.Encoding.UTF8);

        }


        public IActionResult Index1(User user)
        {
            //System.Threading.Thread.Sleep(5000); //停止5秒鐘

            //return Content("Ajax, 你好!!","text/plain", System.Text.Encoding.UTF8);
            if (String.IsNullOrEmpty(user.name))
            {
                user.name = "Ajax";
            }
            return Content($"{user.name}你好,你的年紀是{user.age}!!", "text/plain", System.Text.Encoding.UTF8);
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
        public IActionResult GetImageBytes(int id = 1)
        {
            Member member = _demo.Members.Find(id);
            byte[] img = member.FileData;
            return File(img, "image/jpeg");
        }

        //判斷姓名
        public bool hasexit(string name)
        {

           bool result= _demo.Members.Any(m => m.Name == name);

            //var result = from m in _demo.Members
            //             where m.Name == name
            //             select m.Name;
            //if (result.Count() > 0)
            //{
            //    return true;
            //}
            return result;
        }

        public IActionResult City()
        {
            var cities = _demo.Addresses.Select(a => a.City).Distinct();
            return Json(cities);
        }

        //根據城市名稱讀出鄉鎮區的資料
        public IActionResult Districts(string city)
        {
            var districts = _demo.Addresses.Where(a => a.City == city).Select(a => a.SiteId).Distinct();
            return Json(districts);
        }

        //根據鄉鎮區的資料讀出路名
        public IActionResult Roads(string district)
        {
            var roads = _demo.Addresses.Where(a => a.SiteId == district).Select(a => a.Road);
            return Json(roads);
        }
        public IActionResult Members()
        {
            return Json(_demo.Members);
        }
    }
}
