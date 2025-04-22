using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Courses.Domain.DomainModels;
using Courses.Web.Data;
using Courses.Service.Interface;
using Courses.Service.Implementation;

namespace Courses.Web.Controllers
{
    public class EnrolmentsController : Controller
    {
        private readonly IEnrolmentService _enrolmentService;

        public EnrolmentsController(IEnrolmentService _enrolmentService)
        {
            this._enrolmentService = _enrolmentService;
        }



        // GET: Enrolments
        public IActionResult Index()
        {
            return View(_enrolmentService.GetAll());
        }

        // GET: Enrolments/Details/5
        public IActionResult Details(Guid id)
        {
            var enrollment = _enrolmentService.GetById(id);

            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // GET: Enrolments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Enrolments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,DateEnroled,ReEnrolled,StudentId,CourseId")] Enrolment enrolment)
        {
            if (ModelState.IsValid)
            {
                _enrolmentService.Insert(enrolment);
                return RedirectToAction(nameof(Index));
            }
            return View(enrolment);
        }

        // GET: Enrolments/Edit/5
        public IActionResult Edit(Guid id)
        {
            var enrollment = _enrolmentService.GetById(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            return View(enrollment);
        }

        // POST: Enrolments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid? id, [Bind("Id,DateEnroled,ReEnrolled,StudentId,CourseId")] Enrolment enrolment)
        {
            if (id != enrolment.Id)
            {
                return NotFound();
            }

            _enrolmentService.Update(enrolment);


            return RedirectToAction(nameof(Index));
     
        }

        // GET: Enrolments/Delete/5
        public IActionResult Delete(Guid id)
        {
            var course = _enrolmentService.GetById(id);

            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Enrolments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _enrolmentService.DeleteById(id);

            return RedirectToAction(nameof(Index));
        }

        private bool EnrolmentExists(Guid id)
        {
            var enrollment = _enrolmentService.GetById(id);
            if (enrollment == null)
            {
                return false;
            }
            return true;
        }
    }
}
