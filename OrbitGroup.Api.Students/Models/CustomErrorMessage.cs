using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrbitGroup.Api.Students.Models
{
    public static class CustomErrorMessage
    {
        public const string NotFound = "Not Found";
        public const string ExistingUsername = "User name alredy exists, Operation Canceled";
        public const string DataError = "Data error";
    }
}
