using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Notes.Domain.DAL;
using Notes.Domain.Entities;
using Notes.WebUI.Models;

namespace Notes.WebUI.Controllers
{
    public class NoteController : Controller
    {
        private readonly UnituOfWork unitOfWork;

        public NoteController(UnituOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [Authorize]
        public ActionResult List()
        {
            String login = User.Identity.Name;

            User user = unitOfWork.UserRepository.Get().Where(a => a.Login.Trim() == login).FirstOrDefault();

            var notes = unitOfWork.NoteRepository.Get().Where(a => a.IdUser == user.Id && a.IdNoteStatus == 2);

            return View(notes);
        }

        [Authorize]
        public ActionResult HistoryNotesList()
        {
            String login = User.Identity.Name;

            User user = unitOfWork.UserRepository.Get().Where(a => a.Login.Trim() == login).FirstOrDefault();

            var notes = unitOfWork.NoteRepository.Get().Where(a => a.IdUser == user.Id && a.IdNoteStatus == 1);

            return View(notes);
        }

        //
        // GET: /Node/Create

        [Authorize]
        public PartialViewResult GetFormForCreate()
        {
            var noteViewModel = new CreateNoteViewModel();

            var selectList = unitOfWork.NoteTypeRepository.Get().Select(x => new SelectListItem
            {
                Text = x.Type,
                Value = x.Id.ToString()
            }).ToList();

            noteViewModel.NoteType = selectList;

            return PartialView(noteViewModel);
        } 

        public PartialViewResult GetFormForClearHistoryNotes()
        {
            return PartialView();
        }

        //
        // POST: /Node/Create

        [Authorize]
        [HttpPost]
        public String Create(String data, int idNoteType)
        {
            String statusMessage;
            String jsonString;
            var scriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            CreateNoteViewModel createNoteViewModel = new CreateNoteViewModel();
            createNoteViewModel.Data = data;
            createNoteViewModel.IdNoteType = idNoteType;

            Note note = new Note();
            note.Data = createNoteViewModel.Data;
            note.NoteStatus = unitOfWork.NoteStatusRepository.GetByID(2);
            note.NoteType = unitOfWork.NoteTypeRepository.GetByID(createNoteViewModel.IdNoteType);

            String login = User.Identity.Name;

            User user = unitOfWork.UserRepository.Get().Where(a => a.Login.Trim() == login).FirstOrDefault();
            note.User = user;
            
            try
            {
                ValidateModel(note);

            }
            catch (Exception e)
            {
                statusMessage = "fail";
                var errors = ModelState
                            .Where(x => x.Value.Errors.Count > 0)
                            .Select(x => new { x.Key, x.Value.Errors })
                            .ToArray();

                jsonString = scriptSerializer.Serialize(new { status = statusMessage, errors = errors });
                return jsonString;
            }

            if (ModelState.IsValid)
            {
                unitOfWork.NoteRepository.Insert(note);
                try
                {
                    unitOfWork.Save();
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }

                statusMessage = "success";
                String noteData = note.Data;
                String noteType = note.NoteType.Type;
                int idNote = note.Id;

                jsonString = scriptSerializer.Serialize(new { status = statusMessage, data = noteData, noteType = noteType, idNote = idNote });
                return jsonString;
            }

            statusMessage = "fail";

            jsonString = scriptSerializer.Serialize(new { status = statusMessage });
            return jsonString;
        }


        // POST: /Node/ChangeNodeStatusToHistory/5

        [Authorize]
        [HttpPost]
        public String ChangeNodeStatusToHistory(int idNote)
        {
            String statusMessage;
            String jsonString;
            var scriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            String login = User.Identity.Name;

            User user = unitOfWork.UserRepository.Get().Where(a => a.Login.Trim() == login).FirstOrDefault();

            var note = unitOfWork.NoteRepository.GetByID(idNote);

            if (note.IdUser == user.Id)
            {
                note.IdNoteStatus = 1;
                unitOfWork.NoteRepository.Update(note);
                unitOfWork.Save();

                statusMessage = "success";

                jsonString = scriptSerializer.Serialize(new { status = statusMessage });
                return jsonString;
            }

            statusMessage = "fail";

            jsonString = scriptSerializer.Serialize(new { status = statusMessage });
            return jsonString;
        }

        [Authorize]
        public String ClearHistoryNotes()
        {
            String statusMessage;
            String jsonString;
            var scriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            String login = User.Identity.Name;

            User user = unitOfWork.UserRepository.Get().Where(a => a.Login.Trim() == login).FirstOrDefault();

            IEnumerable<Note> notes = unitOfWork.NoteRepository.Get().Where(a => a.IdUser == user.Id && a.IdNoteStatus == 1);

            if (notes.Any())
            {
                foreach (var note in notes)
                {
                    unitOfWork.NoteRepository.Delete(note);
                }
                unitOfWork.Save();

                statusMessage = "success";

                jsonString = scriptSerializer.Serialize(new { status = statusMessage });
                return jsonString;
            }

            statusMessage = "fail";

            jsonString = scriptSerializer.Serialize(new { status = statusMessage });
            return jsonString;
        }
    }
}
