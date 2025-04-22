using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.Domain.DomainModels;

namespace Courses.Service.Interface
{
    public interface IEnrolmentService
    {
        List<Enrolment> GetAll();
        Enrolment? GetById(Guid id);
        Enrolment Insert(Enrolment enrolment);
        Enrolment Update(Enrolment enrolment);
        Enrolment DeleteById(Guid id);
    }
}
