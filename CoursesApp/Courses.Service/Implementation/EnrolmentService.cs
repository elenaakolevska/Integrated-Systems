using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.Domain.DomainModels;
using Courses.Repository.Interface;
using Courses.Service.Interface;

namespace Courses.Service.Implementation
{
    public class EnrolmentService : IEnrolmentService
    {

        private readonly IRepository<Enrolment> _enrolmentRepository;

        public EnrolmentService(IRepository<Enrolment> enrolmentRepository)
        {
            _enrolmentRepository = enrolmentRepository;
        }

        public Enrolment DeleteById(Guid id)
        {
            var enrollment = GetById(id);
            if (enrollment == null)
            {
                throw new Exception("Enrollment not found");
            }
            _enrolmentRepository.Delete(enrollment);
            return enrollment;
        }

        public List<Enrolment> GetAll()
        {
            return _enrolmentRepository.GetAll(selector: x => x).ToList();
        }

        public Enrolment? GetById(Guid id)
        {
            return _enrolmentRepository.Get(selector: x => x,
                                                             predicate: x => x.Id.Equals(id));
        }

        public Enrolment Insert(Enrolment enrolment)
        {

            enrolment.Id = Guid.NewGuid();
            return _enrolmentRepository.Insert(enrolment);
        }

        public Enrolment Update(Enrolment enrolment)
        {
            return _enrolmentRepository.Update(enrolment);
        }
    }
}
