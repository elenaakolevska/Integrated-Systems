using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.Domain.IdentityModels;
using Courses.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Courses.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<CoursesApplicationUser> entites;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
            this.entites = _context.Set<CoursesApplicationUser>();
        }

        public CoursesApplicationUser GetUserById(string id)
        {
            return entites.First(ent => ent.Id == id);
        }
    }
}
