﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AgendaApp.Controllers
{
    public class AboutController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

    }
}