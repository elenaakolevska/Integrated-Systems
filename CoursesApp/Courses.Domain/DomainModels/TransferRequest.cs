using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.Domain.IdentityModels;

namespace Courses.Domain.DomainModels
{
    public class TransferRequest : BaseEntity
    {
        public DateTime DateCreated { get; set; }  = DateTime.Now;

        public string? CreatedById {  get; set; }
        public  CoursesApplicationUser? CreatedBy { get; set; }

        public  ICollection<CourseTransfer> CourseTransfers { get; set; } = new List<CourseTransfer>();
    }
}
