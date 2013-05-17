using System;
using System.Collections.Generic;
using System.Linq;
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

        public ActionResult List()
        {
            var notes = new List<Note>
                {
                    new Note {Data = "Какой-то текст, который требуется вывести<br>fawerfaerg", NoteStatus = null, NoteType = null, User = null},
                    new Note {Data = "Какой-то текст, которыwefwefй требуется вывести<br>fawerfaerg", NoteStatus = null, NoteType = null, User = null}
                };


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

        public PartialViewResult Create()
        {
            var noteViewModel = new CreateNoteViewModel();

            var a = unitOfWork.NoteTypeRepository.GetByID(1);

            var selectList = unitOfWork.NoteTypeRepository.Get().Select(x => new SelectListItem
            {
                Text = x.Type,
                Value = x.Id.ToString()
            }).ToList();

            noteViewModel.NoteType = selectList;
            noteViewModel.NoteStatus = unitOfWork.NoteStatusRepository.GetByID(2);

            return PartialView(noteViewModel);
        } 

        //
        // POST: /Node/Create

        [HttpPost]
        public ActionResult Create(CreateNoteViewModel createNoteViewModel)
        {
            Note note = new Note();
            note.Data = createNoteViewModel.Data;
            note.NoteStatus = createNoteViewModel.NoteStatus;
            note.NoteType = unitOfWork.NoteTypeRepository.GetByID(createNoteViewModel.IdNoteType);

            User user = unitOfWork.UserRepository.GetByID(1); //TODO get real user
            note.User = user;

            if (ModelState.IsValid)
            {
                unitOfWork.NoteRepository.Insert(note);
                unitOfWork.Save();
                TempData["message"] = "Элемент успешно добавлен!";
            }

            return View();
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
