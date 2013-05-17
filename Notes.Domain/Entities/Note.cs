using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Notes.Domain.Entities
{
    [Table("Note", Schema = "dbo")]
    public class Note
    {
        [Key, HiddenInput(DisplayValue = false)]
        public int Id { get; private set; }

        [Required(ErrorMessage = "Data обязателен.")]
        public string Data { get; set; }

        public virtual NoteType NoteType { get; set; }

        public virtual NoteStatus NoteStatus { get; set; }

        public virtual User User { get; set; }
    }
}
