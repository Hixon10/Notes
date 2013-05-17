using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Notes.Domain.DAL;

namespace Notes.WebUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly UnituOfWork unitOfWork;

        public LoginController(UnituOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}
