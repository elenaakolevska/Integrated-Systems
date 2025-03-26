using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Domain.DomainModels
{
    public class Lecture
    {
        [Key]
        public Guid? Id { get; set; }
        public string? LectureName { get; set; }
        public DateTime? Date { get; set; }
        public Course? Course { get; set; }

    }
}
