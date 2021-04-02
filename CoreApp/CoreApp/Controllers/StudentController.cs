using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoreApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreApp.Controllers
{
    public class StudentController : Controller
    {
        private readonly AppDbContext _context;
        public StudentController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddStudent()
        {
            var student = new Student();
            return View(student);
        }

        [HttpPost]
        public IActionResult AddStudent(Student student, IFormFile StdPhoto)
        {
            //if (StdPhoto != null)
            //{
            //    using (var ms = new MemoryStream())
            //    {

            //        StdPhoto.CopyTo(ms);
            //        student.StudentPhoto = ms.ToArray();
            //    }
            //}

            student.StudentPhoto = GetByteArrayFromImage(StdPhoto);
            _context.Add(student);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        private byte[] GetByteArrayFromImage(IFormFile file)
        {
            using (var target = new MemoryStream())
            {
                file.CopyTo(target);
                return target.ToArray();
            }
        }
    }
}
