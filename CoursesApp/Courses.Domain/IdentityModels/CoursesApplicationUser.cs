using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Courses.Domain.IdentityModels
{
    public class CoursesApplicationUser : IdentityUser
    {
        public string?  Name { get; set; }
        public string? Surname { get; set; }
        public DateTime? DateOfBirth { get; set; }

    }
}
