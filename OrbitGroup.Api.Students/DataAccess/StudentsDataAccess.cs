using Microsoft.EntityFrameworkCore;
using OrbitGroup.Api.Students.Db;
using OrbitGroup.Api.Students.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrbitGroup.Api.Students.DataAccess
{
    public class StudentsDataAccess : IStudentsDataAccess
    {
        private readonly StudentsDbContext context;

        public StudentsDataAccess(StudentsDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(Student _student)
        {
            context.Student.Add(_student);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Student _student)
        {
            context.Student.Remove(_student);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            var result = await context.Student.ToListAsync();
            return result;
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            var result = await context.Student.FirstOrDefaultAsync(
                    s => s.Id == id
                    );
            return result;
        }

        public async Task<Student> GetByUsernameAsync(string username)
        {
            var result = await context.Student.FirstOrDefaultAsync(
                    s => s.Username == username
                    );
            return result;
        }

        public async Task<Student> GetByUsernameWithDifferentIdAsync(string username,int id)
        {
            var result = await context.Student.FirstOrDefaultAsync(
                    s => s.Username == username && s.Id != id
                    );
            return result;
        }

        public async Task UpdateAsync(Student _student)
        {            
            context.Entry(_student).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}
