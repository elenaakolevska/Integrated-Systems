using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.Domain.DomainModels;
using Courses.Repository.Interface;
using Courses.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace Courses.Service.Implementation
{

    public class CourseTransferService : ICourseTransferService
    {
        private readonly IRepository<Enrolment> _enrolmentRepository;
        private readonly IRepository<TransferRequest> _transferRequestRepository;
        private readonly IRepository<CourseTransfer> _courseTransferRepository;
        private readonly IUserRepository _userRepository;

        public CourseTransferService(
            IRepository<Enrolment> enrolmentRepository,
            IRepository<TransferRequest> transferRequestRepository,
            IRepository<CourseTransfer> courseTransferRepository,
            IUserRepository userRepository)
        {
            _enrolmentRepository = enrolmentRepository;
            _transferRequestRepository = transferRequestRepository;
            _courseTransferRepository = courseTransferRepository;
            _userRepository = userRepository;
        }

        public bool TransferCourses(Guid studentId, List<Guid> enrolmentIdsToTransfer, string createdByUserId)
        {
            var enrolments = _enrolmentRepository.GetAll(
                selector: x => x, // враќа целосен Enrolment објект
                predicate: x => enrolmentIdsToTransfer.Contains(x.Id) && x.StudentId == studentId,
                include: x => x.Include(e => e.Course) // ако ти треба да го вклучиш курсот
            ).ToList();

            if (!enrolments.Any())
                return false;

            var transferRequest = new TransferRequest
            {
                Id = Guid.NewGuid(),
                CreatedById = createdByUserId,
                DateCreated = DateTime.Now,
            };
            _transferRequestRepository.Insert(transferRequest);

            foreach (var enrolment in enrolments)
            {
                var courseTransfer = new CourseTransfer
                {
                    Id = Guid.NewGuid(),
                    EnrolmendId = enrolment.Id,
                    Enrolment = enrolment,
                    TransferRequestId = transferRequest.Id,
                    TransferRequest = transferRequest
                };

                _courseTransferRepository.Insert(courseTransfer);
            }

            return true;
        }
    }
    
}
