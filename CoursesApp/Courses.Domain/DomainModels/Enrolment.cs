using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.Domain.IdentityModels;

namespace Courses.Domain.DomainModels
{
	public class Enrolment : BaseEntity
	{
		
        public DateTime DateEnroled { get; set; }
		public Boolean ReEnrolled { get; set; }
		public Guid StudentId {  get; set; }
		public Student? Student { get; set; }
		public Guid CourseId { get; set; }
		public Course? Course { get; set; }

		public string? CreatedById {  get; set; }
		public CoursesApplicationUser? CreatedBy { get; set; }
	}
}
