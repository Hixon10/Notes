using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Notes.Domain.DAL;
using Notes.Domain.Entities;
using Notes.WebUI.Models;

namespace Notes.WebUI.Controllers
{
    public class RegistrationController : Controller
    {
        //

        private readonly UnituOfWork unitOfWork;

        public RegistrationController(UnituOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: /Registration/
        [HttpGet]
        public ViewResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Index(RegistrationUserViewModel registrationUserViewModel)
        {
            //try
            //{
            if (ModelState.IsValid)
            {
                var lg = unitOfWork.UserRepository.Get().Select(x => x.Login).ToList();
                var em = unitOfWork.UserRepository.Get().Select(x => x.Email).ToList();
                int countMatchesLogin = 0;
                int countMatchesEmail = 0;
                foreach (var l in lg)
                {
                    if (l.Trim() != registrationUserViewModel.Login.Trim()) continue;
                    countMatchesLogin = 1;
                    break;
                }
                foreach (var l in em)
                {
                    if (l.Trim() != registrationUserViewModel.Email.Trim()) continue;
                    countMatchesEmail = 1;
                    break;
                }
                if (countMatchesEmail == 1)
                {
                    ModelState.AddModelError("", "Такой емаил уже существует.");
                }
                if (countMatchesLogin == 1)
                {
                    ModelState.AddModelError("", "Такой логин уже существует.");
                }
                var user = new User();
                user.Login = registrationUserViewModel.Login.Trim();
                string newSalt = GenerateSalt();
                user.Hash = GetHashForRegistration(registrationUserViewModel.Password + newSalt);
                user.Email = registrationUserViewModel.Email;
                user.Salt = newSalt;
                if (ModelState.IsValid)
                {
                    unitOfWork.UserRepository.Insert(user);
                    unitOfWork.Save();

                    LoginController loginController = new LoginController(unitOfWork);
                    AuthUserViewModel authUserViewModel = new AuthUserViewModel();
                    authUserViewModel.Login = registrationUserViewModel.Login.Trim();
                    authUserViewModel.Password = registrationUserViewModel.Password;
                    return loginController.ViewAuthFrom(authUserViewModel);
                }
            }
           
            return View();
        }

        private string GenerateSalt()
        {
            string valid = "abcdefghijklmnopqrstuvwxyz1234567890";
            string res = "";
            int len = 9;
            Random rnd = new Random();
            while (0 < len--)
                res += valid[rnd.Next(valid.Length)];
            return res;
        }

        private string GetHashForRegistration(string str)
        {
            string pathDest = str;
            StringBuilder sb = new StringBuilder();
            MD5 md5Hasher = MD5.Create();
            foreach (Byte b in md5Hasher.ComputeHash(Encoding.Default.GetBytes(pathDest)))
                sb.Append(b.ToString("x2").ToLower());
            return sb.ToString();
        }
    }
}
