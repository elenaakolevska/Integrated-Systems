using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.Domain.DomainModels;

namespace Courses.Service.Interface
{
    public interface IEnrollmentService
    {
        List<Enrolment> GetAll();
        Enrolment? GetById(Guid id);
        Enrolment Insert(Enrolment enrollment);
        Enrolment Update(Enrolment enrolment);
        Enrolment DeleteById(Guid id);
    }
}
