using OrbitGroup.Api.Students.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrbitGroup.Api.Students.Interfaces
{
    public interface IStudentsProvider
    {
        Task<(bool IsSuccess, IEnumerable<MStudent> Students, string ErrorMsg)> GetStudentsAsync();
        Task<(bool IsSuccess, MStudent Student, string ErrorMsg)> GetStudentByIdAsync(int id);
        Task<(bool IsSuccess, int Id, string ErrorMsg)> AddStudentAsync(MStudent mStudent);
        Task<(bool IsSuccess, string ErrorMsg)> UpdateStudentAsync(MStudent mStudent);
        Task<(bool IsSuccess, string ErrorMsg)> DeleteStudentAsync(int id);
    }
}
