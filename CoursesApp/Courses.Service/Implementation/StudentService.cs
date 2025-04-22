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
    public class StudentService : IStudentService
    {
        private readonly IRepository<Student> _studentRepository;

        public StudentService(IRepository<Student> studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public Student DeleteById(Guid id)
        {
            var student = GetById(id);
            if (student == null)
            {
                throw new Exception("Student not found");
            }
            _studentRepository.Delete(student);
            return student;
        }

        public List<Student> GetAll()
        {
            return _studentRepository.GetAll(selector: x => x).ToList();
        }

        public Student? GetById(Guid id)
        {
            return _studentRepository.Get(selector: x => x,
                                                     predicate: x => x.Id.Equals(id));
        }

        public Student Insert(Student student)
        {
            student.Id = Guid.NewGuid();
            return _studentRepository.Insert(student);
        }

        public Student Update(Student student)
        {
            return _studentRepository.Update(student);
        }
    }
}
