﻿using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules.FluentValidation;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo2.Controllers
{
    public class RegisterController : Controller
    {
        WriterManager wm = new WriterManager(new EfWriterRepository());

        [HttpGet]
        //sayfa yüklenince çalışacak
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        //sayfada buton tetiklenince çalışacak
        public IActionResult Index(Writer p)
        {
            WriterValidator wv = new WriterValidator();
            ValidationResult results = wv.Validate(p);
            if(results.IsValid)
            {
                p.WriterStatus = true;
                p.WriterAbout = "Deneme Test";
                if (p.WriterPassword == p.WriterPasswordRepeat)
                {
                    wm.TAdd(p);
                    return RedirectToAction("Index", "Blog");
                }   
   
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
    }
}