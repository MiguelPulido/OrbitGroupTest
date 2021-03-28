using OrbitGroup.Api.Students.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrbitGroup.Api.Students.Interfaces
{
    public interface IStudentsDataAccess
    {
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student> GetByIdAsync(int id);
        Task<Student> GetByUsernameAsync(string username);
        Task<Student> GetByUsernameWithDifferentIdAsync(string username, int id);
        Task AddAsync(Student _student);
        Task UpdateAsync(Student _student);
        Task DeleteAsync(Student _student);
    }
}
