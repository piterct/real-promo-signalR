using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RealPromo.API.Models;
using RealPromo.API.Notifications;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RealPromo.API.Controllers
{
    public class HomeController : MainController
    {
        public HomeController(INotificador notificador) : base(notificador)
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Promocao()
        {
            return View();
        }

        public ActionResult CadastrarPromocao()
        {
            return View();
        }

    }
}
