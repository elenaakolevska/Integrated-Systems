using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesAdminApplication.Models
{
    public class TransferRequest 
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }  = DateTime.Now;

        public string? CreatedById {  get; set; }
        public  CoursesApplicationUser? CreatedBy { get; set; }

        public  ICollection<CourseTransfer> CourseTransfers { get; set; } = new List<CourseTransfer>();
    }
}
