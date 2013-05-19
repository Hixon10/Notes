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
        // GET: /Node/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Node/Create

        public PartialViewResult GetFormForCreate()
        {
            var noteViewModel = new CreateNoteViewModel();

            var a = unitOfWork.NoteTypeRepository.GetByID(1);

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

        private T Deserialise<T>(string json)
        {
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(json)))
            {
                var serialiser = new DataContractJsonSerializer(typeof(T));
                return (T)serialiser.ReadObject(ms);
            }
        }
        
        //
        // GET: /Node/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Node/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Node/ChangeNodeStatusToHistory/5
 
        public ActionResult ChangeNodeStatusToHistory(int id)
        {
            return View();
        }

        //
        // POST: /Node/ChangeNodeStatusToHistory/5

        [HttpPost]
        public ActionResult ChangeNodeStatusToHistory(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
