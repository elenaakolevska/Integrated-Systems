using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.Domain.DomainModels;
using Courses.Repository.Interface;

namespace Courses.Service.Interface
{
    public interface ITransferRequestService
    {
        List<TransferRequest> GetAllTransfers();
        TransferRequest GetTransfer(Guid id);
    }
}
