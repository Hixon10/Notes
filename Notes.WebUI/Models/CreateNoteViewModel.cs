using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Notes.Domain.Entities;

namespace Notes.WebUI.Models
{
    public class CreateNoteViewModel
    {
        [Required(ErrorMessage = "Data обязателен.")]
        [DataType(DataType.MultilineText)]
        public string Data { get; set; }

        public IEnumerable<SelectListItem> NoteType { get; set; }
        
        [Required]
        [HiddenInput(DisplayValue = false)]
        public int IdNoteType { get; set; }

        public NoteStatus NoteStatus { get; set; }
    }
}