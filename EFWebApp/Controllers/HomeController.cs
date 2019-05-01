using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EFWebApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.IO;
using EFWebApp.Data;

namespace EFWebApp.Controllers
{
    public class HomeController : Controller
    {
        private IHostingEnvironment _environment;
        private string _connectionString;
        private ImageRepository repo;
        
        public HomeController(IHostingEnvironment environment, IConfiguration configuration)
        {
            _environment = environment;
            _connectionString = configuration.GetConnectionString("ConStr");
            repo = new ImageRepository(_connectionString);
        }

        public IActionResult Index()
        {

            var repo = new ImageRepository(_connectionString);
            IEnumerable<Image> images = repo.GetImages();
            return View(images);
        }

        public IActionResult UploadImage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadImage(IFormFile file, string title)
        {
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            string fullPath = Path.Combine(_environment.WebRootPath, "uploads", fileName);
            using (FileStream stream = new FileStream(fullPath, FileMode.CreateNew))
            {
                file.CopyTo(stream);
            }

            var image = new Image
            {
                Name = fileName,
                Title = title
            };

            repo.SaveImage(image);

            return Redirect("/");
        }

        public IActionResult ViewImage(int id)
        {
            Image i = repo.GetImageById(id);
            return View(i
);
        }

        [HttpPost]
        public IActionResult IncreaseLike(int id)
        {
            repo.IncreaseLike(id);
            return Json(id);
        }

        public IActionResult NumberOfLikes(int id)
        {
            int likes = repo.NumberOfLikes(id);
            return Json(likes);
        }

    }
}
