using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Notes.WebUI.Models
{
    public class RegistrationUserViewModel
    {
        //[StringLength(32, MinimumLength = 3, ErrorMessage = "Длина Login должна быть от 3 до 32 символов!")]
        [Required(ErrorMessage = "Login обязателен.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password обязателен.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "ConfirmPassword обязателен.")]
        [Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Email обязателен.")]
        //[DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email Address")]
        [RegularExpression(@"[\w\d-\.]+@([\w\d-]+(\.[\w\-]+)+)",
        ErrorMessage = "Email is not valid")]
        public string Email { get; set; }
    }
}