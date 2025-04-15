using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Domain.DomainModels
{
	public class Student : BaseEntity
	{
        public string? Name { get; set; }
		public string? Surname { get; set; }
		public DateTime? DateOfBirth { get; set; }
		public string? Index {  get; set; }
		public int EnrollmentYear { get; set; }
	}
}
