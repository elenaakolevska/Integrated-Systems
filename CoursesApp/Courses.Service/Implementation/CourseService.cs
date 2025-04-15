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
    public class CourseService : ICourseService
    {
        private readonly IRepository<Course> _courseRepository;

        public CourseService(IRepository<Course> courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public Course DeleteById(Guid id)
        {
            var course = GetById(id);
            if (course == null)
            {
                throw new Exception("Course not found");
            }
            _courseRepository.Delete(course);
            return course;
        }

        public List<Course> GetAll()
        {
            return _courseRepository.GetAll(selector: x => x).ToList();
        }

        public Course? GetById(Guid id)
        {
            return _courseRepository.Get(selector: x => x,
                                                     predicate: x => x.Id.Equals(id));
        }

        public Course Insert(Course course)
        {
            course.Id = Guid.NewGuid();
            return _courseRepository.Insert(course);
        }

        public Course Update(Course course)
        {
            return _courseRepository.Update(course);
        }
    }
}
