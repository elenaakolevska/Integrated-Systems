using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Domain.DomainModels
{
    public class CourseTransfer : BaseEntity
    {
        public Guid EnrolmendId { get; set; }
        public virtual Enrolment? Enrolment { get; set; }    

        public Guid TransferRequestId { get; set; }
        public virtual TransferRequest? TransferRequest { get; set; }
    }
}
