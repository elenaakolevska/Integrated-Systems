using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesAdminApplication.Models
{
	public class Enrolment 
	{
        public Guid Id { get; set; }
		[Required]
        public DateTime DateEnroled { get; set; }
        [Required]
        public Boolean ReEnrolled { get; set; }
        [Required]
        public Guid StudentId {  get; set; }
        [Required]
        public Student? Student { get; set; }
        [Required]
        public Guid CourseId { get; set; }
        [Required]
        public Course? Course { get; set; }
        [Required]

        public string? CreatedById {  get; set; }
		public CoursesApplicationUser? CreatedBy { get; set; }
	}
}
