using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Notes.WebUI.Models
{
    public class AuthUserViewModel
    {
        [StringLength(32, MinimumLength = 3, ErrorMessage = "Длина Login должна быть от 3 до 32 символов!")]
        [Required(ErrorMessage = "Login обязателен.")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Password обязателен.")]
        public string Password { get; set; }
    }
}