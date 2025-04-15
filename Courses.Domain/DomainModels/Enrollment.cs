using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.Domain.Identity;

namespace Courses.Domain.DomainModels
{
    public class Enrollment
    {
        [Key]
        public Guid? Id { get; set; }
        public DateTime? DateEnrolled { get; set; }
        public Boolean? ReEnrolled { get; set; }
        public string? StudentId { get; set; }
        public Student? Student {  get; set; }
        public string? CoursesId { get; set; }
        public Course? Course { get; set; }
    }
}
