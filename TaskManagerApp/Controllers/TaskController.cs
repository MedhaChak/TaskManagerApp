using Microsoft.AspNetCore.Mvc;
using TaskManagerApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskManagerApp.Models;

namespace TaskManagerApp.Controllers
{
    public class TaskController : Controller
    {
        // 1 making a tool that will talk to the database
        private readonly AppDbContext _context;
        public TaskController(AppDbContext context)
        {
            _context = context;
        }
        

        // 2 getting details of a specific task // 33333

        public IActionResult Details(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task == null) return NotFound();
            return View(task);
            
        }
        // 3 getting the create task page

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //4 posting the created task

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TaskModel model)
        {
            if (!ModelState.IsValid) return View(model);

            _context.Tasks.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        // 5 getting an edit task

        public IActionResult Edit(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task == null) return NotFound();
            return View(task);
        }
        //6 posting an edit task

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TaskModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var task = _context.Tasks.Find(model.Id);
            if (task == null) return NotFound();

            task.Title = model.Title;
            task.Description = model.Description;
            task.IsComplete = model.IsComplete;
            task.DueDate = model.DueDate;
            task.Priority = model.Priority;

            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        // 7 deleting a task

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }
        // 8 toggling task completion status
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ToggleComplete(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task!= null) {
                task.IsComplete = !task.IsComplete;
            _context.SaveChanges(); }
            return RedirectToAction("Index", "Home");
        }
    }
}