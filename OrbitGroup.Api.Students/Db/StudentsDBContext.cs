using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrbitGroup.Api.Students.Db
{
    public class StudentsDbContext : DbContext
    {
        public DbSet<Student> Student { get; set; }

        public StudentsDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
