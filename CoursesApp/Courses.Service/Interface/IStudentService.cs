using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.Domain.DomainModels;

namespace Courses.Service.Interface
{
    public interface IStudentService
    {
        List<Student> GetAll();
        Student? GetById(Guid id);
        Student Insert(Student student);
        Student Update(Student student);
        Student DeleteById(Guid id);
    }
}
