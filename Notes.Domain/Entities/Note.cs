using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Notes.Domain.Validation;

namespace Notes.Domain.Entities
{
    [Table("Note", Schema = "dbo")]
    public class Note
    {
        [Key, HiddenInput(DisplayValue = false)]
        public int Id { get; private set; }

        [Required(ErrorMessage = "Data обязателен.")]
        [MinLengthOrNull(1)]
        public string Data { get; set; }

        [Required(ErrorMessage = "IdNoteType is required.")]
        public int IdNoteType { get; set; }
        [ForeignKey("IdNoteType")]
        public virtual NoteType NoteType { get; set; }

        [Required(ErrorMessage = "IdNoteStatus is required.")]
        public int IdNoteStatus { get; set; }
        [ForeignKey("IdNoteStatus")]
        public virtual NoteStatus NoteStatus { get; set; }

        [Required(ErrorMessage = "IdUser is required.")]
        public int IdUser { get; set; }
        [ForeignKey("IdUser")]
        public virtual User User { get; set; }
    }
}
