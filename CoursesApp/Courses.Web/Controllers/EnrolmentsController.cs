using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Courses.Domain.DomainModels;
using Courses.Web.Data;

namespace Courses.Web.Controllers
{
    public class EnrolmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EnrolmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Enrolments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Enrolments.ToListAsync());
        }

        // GET: Enrolments/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrolment = await _context.Enrolments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (enrolment == null)
            {
                return NotFound();
            }

            return View(enrolment);
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
        public async Task<IActionResult> Create([Bind("Id,DateEnroled,ReEnrolled,StudentId,CourseId")] Enrolment enrolment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enrolment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(enrolment);
        }

        // GET: Enrolments/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrolment = await _context.Enrolments.FindAsync(id);
            if (enrolment == null)
            {
                return NotFound();
            }
            return View(enrolment);
        }

        // POST: Enrolments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, [Bind("Id,DateEnroled,ReEnrolled,StudentId,CourseId")] Enrolment enrolment)
        {
            if (id != enrolment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enrolment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrolmentExists(enrolment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(enrolment);
        }

        // GET: Enrolments/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrolment = await _context.Enrolments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (enrolment == null)
            {
                return NotFound();
            }

            return View(enrolment);
        }

        // POST: Enrolments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid? id)
        {
            var enrolment = await _context.Enrolments.FindAsync(id);
            if (enrolment != null)
            {
                _context.Enrolments.Remove(enrolment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnrolmentExists(Guid? id)
        {
            return _context.Enrolments.Any(e => e.Id == id);
        }
    }
}
