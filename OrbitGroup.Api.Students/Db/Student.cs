using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrbitGroup.Api.Students.Db
{
    public class Student
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Career { get; set; }
    }
}
