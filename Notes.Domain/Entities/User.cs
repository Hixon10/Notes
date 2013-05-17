using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Notes.Domain.Entities
{
    public class User
    {
        [Key, HiddenInput(DisplayValue = false)]
        public int Id { get; private set; }

        [StringLength(50, MinimumLength = 6, ErrorMessage = "Длина Email должна быть от 6 до 50 символов!")]
        [Required(ErrorMessage = "Email обязателен.")]
        public string Email { get; set; }

        [StringLength(32, MinimumLength = 3, ErrorMessage = "Длина Login должна быть от 3 до 32 символов!")]
        [Required(ErrorMessage = "Login обязателен.")]
        public string Login { get; set; }

        [StringLength(32, MinimumLength = 32, ErrorMessage = "Длина Hash должна быть от 32 до 32 символов!")]
        [Required(ErrorMessage = "Hash обязателен.")]
        [HiddenInput(DisplayValue = false)]
        public string Hash { get; set; }

        [StringLength(9, MinimumLength = 9, ErrorMessage = "Длина Salt должна быть от 9 до 9 символов!")]
        [Required(ErrorMessage = "Salt обязателен.")]
        [HiddenInput(DisplayValue = false)]
        public string Salt { get; set; }
    }
}
