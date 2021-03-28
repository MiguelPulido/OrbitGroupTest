using OrbitGroup.Api.Students.Db;
using OrbitGroup.Api.Students.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrbitGroup.Api.Students.Profiles
{
    public class StudentsProfile : AutoMapper.Profile
    {
        public StudentsProfile()
        {
            CreateMap<Student, MStudent>();
            CreateMap<MStudent, Student>();
        }
        
    }
}
