﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SistemaAcademico.APP.Controllers
{
    public class DisciplinaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
