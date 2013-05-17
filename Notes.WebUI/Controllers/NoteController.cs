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

            var selectList = unitOfWork.NoteStatusRepository.Get().Select(x => new SelectListItem
            {
                Text = x.Status,
                Value = x.Id.ToString()
            }).ToList();

            noteViewModel.NoteType = selectList;
            noteViewModel.NoteStatus = unitOfWork.NoteStatusRepository.GetByID(2);

            return PartialView(noteViewModel);
        } 

        //
        // POST: /Node/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
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
