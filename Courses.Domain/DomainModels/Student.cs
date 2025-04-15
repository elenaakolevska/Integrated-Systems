using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Domain.DomainModels
{
    public class Student
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public DateTime? DateOfBirth { get; set; }
        [Required]
        public string? Index { get; set; }
        public int EnrollmentYear { get; set; }



    }
}
