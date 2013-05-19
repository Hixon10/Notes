using System;
using System.Collections.Generic;
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

        public ActionResult List(int idNoteStatus = 2)
        {
            if (idNoteStatus != 1 && idNoteStatus != 2) idNoteStatus = 2;

            User user = unitOfWork.UserRepository.GetByID(1); //TODO get real user

            var notes = unitOfWork.NoteRepository.Get().Where(a => a.IdUser == user.Id && a.IdNoteStatus == idNoteStatus);

            return View(notes);
        }


        //
        // GET: /Node/Create

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

        //
        // POST: /Node/Create

        [HttpPost]
        public String Create(String data, int idNoteType)
        {
            CreateNoteViewModel createNoteViewModel = new CreateNoteViewModel();
            createNoteViewModel.Data = data;
            createNoteViewModel.IdNoteType = idNoteType;

            Note note = new Note();
            note.Data = createNoteViewModel.Data;
            note.NoteStatus = unitOfWork.NoteStatusRepository.GetByID(2);
            note.NoteType = unitOfWork.NoteTypeRepository.GetByID(createNoteViewModel.IdNoteType);

            User user = unitOfWork.UserRepository.GetByID(1); //TODO get real user
            note.User = user;

            String statusMessage;
            String jsonString;
            var scriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            if (ModelState.IsValid)
            {
                unitOfWork.NoteRepository.Insert(note);
                unitOfWork.Save();

                statusMessage = "success";

                jsonString = scriptSerializer.Serialize(new {status = statusMessage});
                return jsonString;
            }

            statusMessage = "fail";

            jsonString = scriptSerializer.Serialize(new { status = statusMessage });
            return jsonString;
        }


        // POST: /Node/ChangeNodeStatusToHistory/5

        [HttpPost]
        public String ChangeNodeStatusToHistory(int idNote)
        {
            String statusMessage;
            String jsonString;
            var scriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            //TODO get real user
            var user = unitOfWork.UserRepository.GetByID(1);

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
    }
}
