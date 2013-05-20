using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using Notes.Domain.DAL;
using Notes.Domain.Entities;
using Notes.WebUI.Models;

namespace Notes.WebUI.Controllers
{
    public class LoginController : Controller
    {
        private UnituOfWork unitOfWork;
        private string UserName = string.Empty;
        
        public LoginController(UnituOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public string GetName()
        {
            return UserName;
        }

        [HttpGet]
        public ViewResult ViewAuthFrom()
        {
            return View();
        }


        [HttpPost]
        public ActionResult ViewAuthFrom(AuthUserViewModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int checkLo = 0;
                    int chekPas = 0;
                    string md5Result = string.Empty;
                    int fff = 0;
                    var selectList = unitOfWork.UserRepository.Get().Select(x => x.Login).ToList();
                    foreach (var r in selectList)
                    {
                        if (r.Trim() != user.Login.Trim())
                        {
                            fff++; continue;
                        }
                        checkLo = 1;
                        break;
                    }
                    
                    var hash = unitOfWork.UserRepository.Get().Select(x => x.Hash).ToList();
                    var salt = unitOfWork.UserRepository.Get().Select(x => x.Salt).ToList();

                    foreach (var h in hash)
                    {
                        foreach (var s in salt)
                        {
                            md5Result = GetHash(user.Password + s);

                            if (md5Result.Trim() != h.Trim()) continue;
                            chekPas = 1;
                            break;
                        }
                    }

                    if (checkLo == 1 && chekPas == 1)
                    {
                        FormsAuthentication.SetAuthCookie(user.Login.Trim(), false);
                        UserName = user.Login.Trim();
                        return RedirectToAction("List", "Note");
                    }
                    if (checkLo == 0 && chekPas == 1)
                    {
                        ModelState.AddModelError("", "Вы ввели неверный логин.");
                        //return Redirect("Index");
                    }
                    else if (checkLo == 1 && chekPas == 0)
                    {
                        ModelState.AddModelError("", "Вы ввели неверный пароль.");
                    }
                    else if (checkLo == 0 && chekPas == 0)
                    {
                        ModelState.AddModelError("", "Вы ввели неверный логин и пароль.");

                    }
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "При сохранении возникла ошибка.");
            }
            return View();
        }

        private static string GetHash(string str)
        {
            string pathDest = str.Trim();
            StringBuilder sb = new StringBuilder();
            MD5 md5Hasher = MD5.Create();
            foreach (Byte b in md5Hasher.ComputeHash(Encoding.Default.GetBytes(pathDest)))
                sb.Append(b.ToString("x2").ToLower());
            return sb.ToString();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("ViewAuthFrom", "Login");
        }
    }
}