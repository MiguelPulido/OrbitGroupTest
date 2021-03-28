using AutoMapper;
using Microsoft.Extensions.Logging;
using OrbitGroup.Api.Students.DataAccess;
using OrbitGroup.Api.Students.Db;
using OrbitGroup.Api.Students.Interfaces;
using OrbitGroup.Api.Students.Models;
using OrbitGroup.Api.Students.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrbitGroup.Api.Students.Providers
{
    public class StudentsProvider : IStudentsProvider
    {
        private readonly IStudentsDataAccess studentsDataAccess;
        private readonly IMapper mapper;
        private readonly ILogger logger;
        private readonly StudentsValidator _studentsValidator;

        public StudentsProvider(IStudentsDataAccess studentsDataAccess, IMapper mapper, ILogger<StudentsProvider> logger, StudentsValidator _studentsValidator)
        {
            this.studentsDataAccess = studentsDataAccess;
            this.mapper = mapper;
            this.logger = logger;
            this._studentsValidator = _studentsValidator;
        }

        public async Task<(bool IsSuccess, int Id, string ErrorMsg)> AddStudentAsync(MStudent mStudent)
        {
            try
            {
                var newStudent = MapFromStudentModelToStudentEntity(mStudent);
                if (!_studentsValidator.areStudentsFieldsValid(newStudent))
                {
                    return (false, -1, CustomErrorMessage.DataError);
                }

                if (await IsStudentUsernameFound(newStudent))
                {
                    return (false, -1, CustomErrorMessage.ExistingUsername);
                }

                newStudent.Id = 0;//Set new student ID to 0 to enable the autoincrement in db

                await studentsDataAccess.AddAsync(newStudent);

                var createdStudent = await studentsDataAccess.GetByUsernameAsync(newStudent.Username);
                if (IsStudentFound(createdStudent))
                {
                    var result = MapFromStudentEntityToStudentModel(createdStudent);
                    return (true, result.Id, null);
                }
                return (false, -1, CustomErrorMessage.NotFound);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                throw;
            }
        }

        public async Task<(bool IsSuccess, string ErrorMsg)> DeleteStudentAsync(int id)
        {
            try
            {
                var student = await studentsDataAccess.GetByIdAsync(id);
                if (IsStudentFound(student))
                {
                    await studentsDataAccess.DeleteAsync(student);
                    return (true, null);
                }
                return (false, CustomErrorMessage.NotFound);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                throw;
            }
        }

        public async Task<(bool IsSuccess, MStudent Student, string ErrorMsg)> GetStudentByIdAsync(int id)
        {
            try
            {
                var student = await studentsDataAccess.GetByIdAsync(id);
                if (IsStudentFound(student))
                {
                    var result = MapFromStudentEntityToStudentModel(student);
                    return (true, result, null);
                }
                return (false, null, CustomErrorMessage.NotFound);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                throw;
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<MStudent> Students, string ErrorMsg)> GetStudentsAsync()
        {
            try
            {
                var students = await studentsDataAccess.GetAllAsync();
                if (AreStudentsFound(students))
                {
                    var result = MapFromStudentEntityToStudentModel(students);
                    return (true, result, null);
                }
                return (false, null, CustomErrorMessage.NotFound);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                throw;
            }
        }

        public async Task<(bool IsSuccess, string ErrorMsg)> UpdateStudentAsync(MStudent mStudent)
        {
            try
            {
                var student = MapFromStudentModelToStudentEntity(mStudent);
                if (!_studentsValidator.areStudentsFieldsValid(student))
                {
                    return (false, CustomErrorMessage.DataError);
                }

                if (!(await IsStudentUsernameUnique(student)))
                {
                    return (false, CustomErrorMessage.ExistingUsername);
                }

                var existingStudent = await studentsDataAccess.GetByIdAsync(student.Id);
                if (IsStudentFound(existingStudent))
                {
                    await studentsDataAccess.UpdateAsync(student);
                    return (true, null);
                }

                return (false, CustomErrorMessage.NotFound);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                throw;
            }
        }

        private MStudent MapFromStudentEntityToStudentModel(Student _student)
        {
            var result = mapper.Map<Student, MStudent>(_student);
            return result;
        }

        private IEnumerable<MStudent> MapFromStudentEntityToStudentModel(IEnumerable<Student> _students)
        {
            var result = mapper.Map<IEnumerable<Student>, IEnumerable<MStudent>>(_students);
            return result;
        }

        private Student MapFromStudentModelToStudentEntity(MStudent _student)
        {
            var result = mapper.Map<MStudent, Student>(_student);
            return result;
        }

        private IEnumerable<Student> MapFromStudentModelToStudentEntity(IEnumerable<MStudent> _students)
        {
            var result = mapper.Map< IEnumerable<MStudent>, IEnumerable<Student>>(_students);
            return result;
        }

        public bool IsStudentFound(Student student)
        {
            return student != null;
        }

        public bool AreStudentsFound(IEnumerable<Student> students)
        {
            return students != null && students.Any();
        }

        private async Task<bool> IsStudentUsernameFound(Student student)
        {
            var studentFounded = await studentsDataAccess.GetByUsernameAsync(student.Username);
            return IsStudentFound(studentFounded);
        }

        private async Task<bool> IsStudentUsernameUnique(Student student)
        {
            var studentFounded = await studentsDataAccess.GetByUsernameWithDifferentIdAsync(student.Username, student.Id);
            return !IsStudentFound(studentFounded);
        }
    }
}
