using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Domain.DomainModels
{
	public class Enrolment : BaseEntity
	{
		
        public DateTime DateEnroled { get; set; }
		public Boolean ReEnrolled { get; set; }
		public int StudentId {  get; set; }
		public Student Student { get; set; }
		public int CourseId { get; set; }
		public Course Course { get; set; }

	}
}
