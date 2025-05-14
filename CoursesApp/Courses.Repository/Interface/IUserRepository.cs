using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.Domain.IdentityModels;

namespace Courses.Repository.Interface
{
    public interface IUserRepository
    {
        CoursesApplicationUser GetUserById(string id);
    }
}
