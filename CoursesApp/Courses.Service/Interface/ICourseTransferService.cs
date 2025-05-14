using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Service.Interface
{
    public interface ICourseTransferService
    {
        bool TransferCourses(Guid studentId, List<Guid> enrolmentIdsToTransfer, string createdByUserId);
    }
}
