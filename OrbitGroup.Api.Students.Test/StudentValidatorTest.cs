using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrbitGroup.Api.Students.Db;
using OrbitGroup.Api.Students.Validators;

namespace OrbitGroup.Api.Students.Test
{
    [TestClass]
    public class StudentValidatorTest
    {
        private readonly StudentsValidator _studentValidator;

        public StudentValidatorTest()
        {
            _studentValidator = new StudentsValidator();
        }
        private Student CreateValidStudent()
        {
            var student = new Student();
            student.Username = "Username";
            student.FirstName = "FirstName";
            student.LastName = "LastName";
            student.Age = 20;
            student.Career = "Career";

            return student;
        }

        [TestMethod]
        public void AreStudentsFieldsValid_ShouldBeValid_WhenAllFieldsAreVAlid()
        {
            var student = CreateValidStudent();

            var result = _studentValidator.AreStudentsFieldsValid(student);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AreStudentsFieldsValid_ShouldBeInvalid_WhenUsernameIsNull()
        {
            var student = CreateValidStudent();
            student.Username = null;

            var result = _studentValidator.AreStudentsFieldsValid(student);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AreStudentsFieldsValid_ShouldBeInvalid_WhenFirstNameIsNull()
        {
            var student = CreateValidStudent();
            student.FirstName = null;

            var result = _studentValidator.AreStudentsFieldsValid(student);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AreStudentsFieldsValid_ShouldBeInvalid_WhenLastNameIsNull()
        {
            var student = CreateValidStudent();
            student.LastName = null;

            var result = _studentValidator.AreStudentsFieldsValid(student);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AreStudentsFieldsValid_ShouldBeInvalid_WhenCareerIsNull()
        {
            var student = CreateValidStudent();
            student.Career = null;

            var result = _studentValidator.AreStudentsFieldsValid(student);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AreStudentsFieldsValid_ShouldBeInvalid_WhenAgeIsLowerThanMinValue()
        {
            var student = CreateValidStudent();
            student.Age = 0;

            var result = _studentValidator.AreStudentsFieldsValid(student);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AreStudentsFieldsValid_ShouldBeInvalid_WhenAgeIsNegative()
        {
            var student = CreateValidStudent();
            student.Age = -1;

            var result = _studentValidator.AreStudentsFieldsValid(student);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AreStudentsFieldsValid_ShouldBeInvalid_WhenUsernameIsLongerThanMaxLength()
        {
            var student = CreateValidStudent();
            student.Username = "aaaaaaaaaabbbbbbbbbbc";

            var result = _studentValidator.AreStudentsFieldsValid(student);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AreStudentsFieldsValid_ShouldBeInvalid_WhenFirstNameIsLongerThanMaxLength()
        {
            var student = CreateValidStudent();
            student.FirstName = "aaaaaaaaaabbbbbbbbbbc";

            var result = _studentValidator.AreStudentsFieldsValid(student);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AreStudentsFieldsValid_ShouldBeInvalid_WhenLastNameIsLongerThanMaxLength()
        {
            var student = CreateValidStudent();
            student.LastName = "aaaaaaaaaabbbbbbbbbbc";

            var result = _studentValidator.AreStudentsFieldsValid(student);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AreStudentsFieldsValid_ShouldBeInvalid_WhenCareerIsLongerThanMaxLength()
        {
            var student = CreateValidStudent();
            student.Career = "aaaaaaaaaabbbbbbbbbbccccccccccddddddddddeeeeeeeeeef";

            var result = _studentValidator.AreStudentsFieldsValid(student);

            Assert.IsFalse(result);
        }
    }
}
