using OrbitGroup.Api.Students.Db;
using OrbitGroup.Api.Students.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrbitGroup.Api.Students.Validators
{
    public class StudentsValidator
    {
        private const int UsernameMaxLength = 20;
        private const int UsernameMinLength = 1;
        private const int FirstNameMaxLength = 20;
        private const int FirstNameMinLength = 0;
        private const int LastNameMaxLength = 20;
        private const int LastNameMinLength = 0;
        private const int CareerMaxLength = 50;
        private const int CareerMinLength = 0;
        private const int AgeMinValue = 1;

        public bool AreStudentsFieldsValid(Student student)
        {
            if (!IsUsernameValid(student.Username))
            {
                return false;
            }
            if (!IsFirstNameValid(student.FirstName))
            {
                return false;
            }
            if (!IsLastNameValid(student.LastName))
            {
                return false;
            }
            if (!IsCareerValid(student.Career))
            {
                return false;
            }
            if (!IsAgeValid(student.Age))
            {
                return false;
            }
            return true;
        }

        private bool IsUsernameValid(string field)
        {
            return IsStringFieldValid(field, UsernameMaxLength, UsernameMinLength);
        }

        private bool IsFirstNameValid(string field)
        {
            return IsStringFieldValid(field, FirstNameMaxLength, FirstNameMinLength);
        }

        private bool IsLastNameValid(string field)
        {
            return IsStringFieldValid(field, LastNameMaxLength, LastNameMinLength);
        }

        private bool IsCareerValid(string field)
        {
            return IsStringFieldValid(field, CareerMaxLength, CareerMinLength);
        }

        private bool IsAgeValid(int field)
        {
            if (field < AgeMinValue)
            {
                return false;
            }
            return true;
        }

        private bool IsStringFieldValid(string field, int maxLength, int minLength)
        {
            if (field is null)
            {
                return false;
            }
            if (field.Length <minLength)
            {
                return false;
            }
            if (field.Length > maxLength)
            {
                return false;
            }
            return true;

        }

    }
}
