using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.Domain.DomainModels;

namespace Courses.Service.Interface
{
    public interface ILectureService
    {
        List<Lecture> GetAll();
        Lecture? GetById(Guid id);
        Lecture Insert(Lecture lecture);
        Lecture Update(Lecture lecture);
        Lecture DeleteById(Guid id);
    }
}
