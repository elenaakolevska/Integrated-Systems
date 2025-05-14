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
    public class TransferRequestService : ITransferRequestService
    {
        private readonly IRepository<TransferRequest> _transferRequestRepository;

        public TransferRequestService(IRepository<TransferRequest> transferRequestRepository)
        {
            _transferRequestRepository = transferRequestRepository;
        }


        public List<TransferRequest> GetAllTransfers()
        {
            return _transferRequestRepository.GetAll(
                selector: x => x,
                include: x => x.Include(z => z.CourseTransfers)
                               .ThenInclude(z => z.Enrolment)
                                   .ThenInclude(e => e.Course) // ➕ Додадено ова
                               .Include(z => z.CreatedBy)
            ).ToList();
        }

        public TransferRequest GetTransfer(Guid Id)
        {
            return _transferRequestRepository.Get(
                selector: x => x,
                predicate: x => x.Id.Equals(Id),
                include: x => x.Include(z => z.CourseTransfers)
                               .ThenInclude(z => z.Enrolment)
                                   .ThenInclude(e => e.Course)
                               .Include(z => z.CreatedBy)
            );
        }


    }



}
