using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Notes.Domain.Entities
{
    public class NoteStatus
    {
        [Key, HiddenInput(DisplayValue = false)]
        public int Id { get; private set; }

        [StringLength(49, MinimumLength = 3, ErrorMessage = "Длина Status должна быть от 3 до 49 символов!")]
        [Required(ErrorMessage = "Status обязателен.")]
        public string Status { get; private set; }
    }
}
