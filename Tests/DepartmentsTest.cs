using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Registrar
{
    public class DepartmentTest : IDisposable
    {
        public DepartmentTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=registrar_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void Test_DepartmentsEmptyAtFirst()
        {
            //Arrange, Act
            int result = Department.GetAll().Count;

            //Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void Test_Save_AssignsIdToDepartmentObject()
        {
            //Arrange
            Department testDepartment = new Department("English");
            testDepartment.Save();

            //Act
            Department savedDepartment = Department.GetAll()[0];

            int result = savedDepartment.GetId();
            int testId = testDepartment.GetId();

            //Assert
            Assert.Equal(testId, result);
        }

        [Fact]
        public void Test_Save()
        {
            //Arrange
            Department testDepartment = new Department("English");
            testDepartment.Save();

            //Act
            List<Department> result = Department.GetAll();
            List<Department> testList = new List<Department>{testDepartment};

            //Assert
            Assert.Equal(testList, result);
        }

        [Fact]
        public void Test_Add_AddStudentToDepartment()
        {
            //Arrange
            Department testDepartment = new Department("English");
            testDepartment.Save();
            Student testStudent = new Student("Britton", "2010-09-01", testDepartment.GetId());
            testStudent.Save();

            //Act
            testDepartment.AddStudent(testStudent.GetId());
            int result = testDepartment.GetId();
            int expected = testStudent.GetDepartmentId();

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_Add_AddCourseToDepartment()
        {
            //Arrange
            Department testDepartment = new Department("English");
            testDepartment.Save();
            Course testCourse = new Course("English", "ENGL120", testDepartment.GetId());
            testCourse.Save();

            //Act
            testDepartment.AddCourse(testCourse.GetId());
            int result = testDepartment.GetId();
            int expected = testCourse.GetDepartmentId();

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_GetStudents_StudentsInDepartment()
        {
            Department testDepartment = new Department("English");
            testDepartment.Save();
            Student testStudent1 = new Student("Britton", "2010-09-01", testDepartment.GetId());
            testStudent1.Save();
            Student testStudent2 = new Student("Rachel", "1999-09-01", testDepartment.GetId());
            testStudent2.Save();

            List<Student> output = testDepartment.GetStudents();
            List<Student> expected = new List<Student>{testStudent1, testStudent2};

            Assert.Equal(expected, output);
        }

        [Fact]
        public void Test_GetCourses_CoursesInDepartment()
        {
            Department testDepartment = new Department("English");
            testDepartment.Save();
            Course testCourse1 = new Course("English", "ENGL120", testDepartment.GetId());
            testCourse1.Save();
            Course testCourse2 = new Course("Math", "MATL120", testDepartment.GetId());
            testCourse2.Save();

            List<Course> output = testDepartment.GetCourses();
            List<Course> expected = new List<Course>{testCourse1, testCourse2};

            Assert.Equal(expected, output);
        }

        public void Dispose()
        {
            Student.DeleteAll();
            Department.DeleteAll();
            Course.DeleteAll();
        }
    }
}
