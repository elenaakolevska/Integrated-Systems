using Courses.Domain.IdentityModels;
using Courses.Service.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Linq;
using System;

namespace Courses.Web.Controllers
{
    public class CourseTransferController : Controller
    {
        private readonly ICourseTransferService _courseTransferService;
        private readonly UserManager<CoursesApplicationUser> _userManager;

        public CourseTransferController(ICourseTransferService courseTransferService, UserManager<CoursesApplicationUser> userManager)
        {
            _courseTransferService = courseTransferService;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult TransferNow()
        {
            return Content("Transfer GET works");
        }

        [HttpPost]
        public async Task<IActionResult> TransferNow(List<Guid> enrolmentIds)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                throw new Exception("You must be logged in.");
            }

            // Optional: validate if the list has at least one element
            if (enrolmentIds == null || !enrolmentIds.Any())
            {
                TempData["Error"] = "No enrolments selected for transfer.";
                return RedirectToAction("Index", "Enrolments");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                TempData["Error"] = "User not found.";
                return RedirectToAction("Index", "Enrolments");
            }

            var studentId = Guid.Parse(user.Id); // <- оваа линија ја поправаме

            var success = _courseTransferService.TransferCourses(
                studentId: studentId,
                enrolmentIdsToTransfer: enrolmentIds,
                createdByUserId: userId
            );


            if (!success)
            {
                TempData["Error"] = "Could not process course transfer.";
                return RedirectToAction("Index", "Enrolments");
            }

            return RedirectToAction("Index", "TransferRequests");
        }
    }
}
