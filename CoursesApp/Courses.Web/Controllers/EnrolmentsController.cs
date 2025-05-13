using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Courses.Domain.DomainModels;
using Courses.Service.Interface;
using Courses.Service.Implementation;
using Microsoft.AspNetCore.Identity;
using Courses.Domain.IdentityModels;
using System.Security.Claims;
using Courses.Repository.Interface;


namespace Courses.Web.Controllers
{
    public class EnrolmentsController : Controller
    {
        private readonly IEnrolmentService _enrolmentService;
        private readonly IStudentService _studentService;
        private readonly ICourseService _courseService;
        private readonly UserManager<CoursesApplicationUser> _userManager;

		private readonly IRepository<TransferRequest> _transferRequestRepo;
		private readonly IRepository<CourseTransfer> _courseTransferRepo;


		public EnrolmentsController(
	            IEnrolmentService enrolmentService, IStudentService studentService,
                ICourseService courseService,
                UserManager<CoursesApplicationUser> userManager,
	            IRepository<TransferRequest> transferRequestRepo,
	            IRepository<CourseTransfer> courseTransferRepo)
		{
			_enrolmentService = enrolmentService;
            _studentService = studentService;
            _courseService = courseService;
			_userManager = userManager;
			_transferRequestRepo = transferRequestRepo;
			_courseTransferRepo = courseTransferRepo;
		}



		// GET: Enrolments
		public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var enrolments = _enrolmentService.GetAllForUser(userId);
            return View(enrolments);
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
        // GET: Enrolments/Create
        // GET: Enrolments/Create
        public IActionResult Create()
        {
            var students = _studentService.GetAll();  // Ова треба да врати колекција на студенти
            var courses = _courseService.GetAll();   // Ова треба да врати колекција на курсеви

            // Пренесувате податоци во ViewBag како SelectList
            ViewBag.Students = new SelectList(students, "Id", "Name");
            ViewBag.Courses = new SelectList(courses, "Id", "CourseName");

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
                enrolment.CreatedById = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _enrolmentService.Insert(enrolment);
                return RedirectToAction(nameof(Index));
            }
            var students = _studentService.GetAll();
            var courses = _courseService.GetAll();
            ViewBag.Students = new SelectList(students, "Id", "Name");
            ViewBag.Courses = new SelectList(courses, "Id", "CourseName");

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
            var enrolment = _enrolmentService.GetById(id);

            if (enrolment == null)
            {
                return NotFound();
            }

            return View(enrolment);
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
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Transfer()
		{
			var userId = _userManager.GetUserId(User);
			var enrolments = _enrolmentService.GetAllForUser(userId);

			if (enrolments == null || !enrolments.Any())
			{
				TempData["Message"] = "Нема енролменти за трансфер.";
				return RedirectToAction(nameof(Index));
			}

			var transferRequest = new TransferRequest
			{
				CreatedById = userId,
				DateCreated = DateTime.Now,
			};

			_transferRequestRepo.Insert(transferRequest);

			foreach (var enrolment in enrolments)
			{
				var transfer = new CourseTransfer
				{
					EnrolmendId = enrolment.Id,
					Enrolment = enrolment,
					TransferRequestId = transferRequest.Id,
					TransferRequest = transferRequest
				};

				_courseTransferRepo.Insert(transfer);
				_enrolmentService.DeleteById(enrolment.Id);
			}

			return RedirectToAction("TransferDetails", new { id = transferRequest.Id });
		}

		public IActionResult TransferDetails(Guid id)
		{
            var transferRequest = _transferRequestRepo.Get(
    selector: tr => tr,
    predicate: tr => tr.Id == id,
    include: source => source
        .Include(tr => tr.CourseTransfers)
        .ThenInclude(ct => ct.Enrolment)
            .ThenInclude(e => e.Student)
        .Include(tr => tr.CourseTransfers)
        .ThenInclude(ct => ct.Enrolment)
            .ThenInclude(e => e.Course)
);



            if (transferRequest == null)
			            {
				            return NotFound();
			            }

			return View(transferRequest);
		}


	}
}
