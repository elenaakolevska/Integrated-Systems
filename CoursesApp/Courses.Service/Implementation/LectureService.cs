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
    public class LectureService : ILectureService
    {
        private readonly IRepository<Lecture> _lectureRepository;

        public LectureService(IRepository<Lecture> lectureRepository)
        {
            _lectureRepository = lectureRepository;
        }

        public Lecture DeleteById(Guid id)
        {
            var lecture = GetById(id);
            if (lecture == null)
            {
                throw new Exception("Lecture not found");
            }
            _lectureRepository.Delete(lecture);
            return lecture;
        }

   

        public List<Lecture> GetAll()
        {
            return _lectureRepository.GetAll(selector: x => x).ToList();
        }

        public Lecture? GetById(Guid id)
        {
            return _lectureRepository.Get(selector: x => x,
                                                      predicate: x => x.Id.Equals(id));
        }

        public Lecture Insert(Lecture lecture)
        {
            lecture.Id = Guid.NewGuid();
            return _lectureRepository.Insert(lecture);
        }

        public Lecture Update(Lecture lecture)
        {
            return _lectureRepository.Update(lecture);
        }
    }
}
