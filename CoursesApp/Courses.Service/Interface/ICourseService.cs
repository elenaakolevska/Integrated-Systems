using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.Domain.DomainModels;

namespace Courses.Service.Interface
{
    public interface ICourseService
    {
        List<Course> GetAll();
        Course? GetById(Guid id);
        Course Insert(Course course);
        Course Update(Course course);
        Course DeleteById(Guid id);
        
    }
}
