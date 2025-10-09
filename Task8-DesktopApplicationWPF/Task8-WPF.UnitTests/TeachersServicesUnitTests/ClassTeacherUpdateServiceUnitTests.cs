using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.BAL.Services.TeachersServices;
using Task8_WPF.DAL;
using Task8_WPF.DAL.Entities;

namespace Task8_WPF.UnitTests.TeachersServicesUnitTests;

[TestClass]
public class ClassTeacherUpdateServiceUnitTests
{
    [TestMethod]
    public void Test_UpdateTeacher_PositiveCase()
    {
        var options = new DbContextOptionsBuilder<WpfAppDbContext>()
            .UseInMemoryDatabase(databaseName: "MockDb30HasData")
            .Options;

        using (var context = new WpfAppDbContext(options))
        {
            var testCourse1 = new Course { Name = "Test course", Description = "This is test course" };
            var testTeacher1 = new Teacher { Name = "First", Surname = "Teacher" };
            var testTeacher2 = new Teacher { Name = "Teacher", Surname = "Third" };
            var testGroup1 = new Group { Name = "TestGrp-01", Course = testCourse1, Teacher = testTeacher1 };
            var testGroup2 = new Group { Name = "TestGrp-02", Course = testCourse1, Teacher = testTeacher1 };
            var testGroup3 = new Group { Name = "TestGrp-03", Course = testCourse1, Teacher = testTeacher2 };
            var testGroup4 = new Group { Name = "TestGrp-04", Course = testCourse1, Teacher = testTeacher2 };
            var testGroup5 = new Group { Name = "TestGrp-05", Course = testCourse1, Teacher = testTeacher2 };

            context.Courses.AddRange(testCourse1);
            context.Teachers.AddRange(testTeacher1, testTeacher2);
            context.Groups.AddRange(testGroup1, testGroup2, testGroup3, testGroup4, testGroup5);

            context.SaveChanges();
        }

        using (var context = new WpfAppDbContext(options))
        {
            var teacherNewName = "Second";
            var teacherNewSurname = "Teacher";
            var selectedTeacher = new TeacherDto { Name = "Teacher", Surname = "Third" };
            var updateTeacherService = new TeacherUpdateService(options);
            updateTeacherService.UpdateTeacher(teacherNewName, teacherNewSurname, selectedTeacher);

            var expectedTeacherValue = new TeacherDto { Name = "Second", Surname = "Teacher" };

            var actualTeacher = context.Teachers
                .FirstOrDefault(t => t.Name + " " + t.Surname == teacherNewName + " " + teacherNewSurname);

            var actualTeacherValue = new TeacherDto
            {
                Name = actualTeacher.Name,
                Surname = actualTeacher.Surname
            };

            actualTeacherValue.Should().BeEquivalentTo(expectedTeacherValue);
        }
    }

    [TestMethod]
    public void Test_UpdateTeacher_TeacherNotFoundCase()
    {
        var options = new DbContextOptionsBuilder<WpfAppDbContext>()
            .UseInMemoryDatabase(databaseName: "MockDb31HasData")
            .Options;

        using (var context = new WpfAppDbContext(options))
        {
            var testCourse1 = new Course { Name = "Test course", Description = "This is test course" };
            var testTeacher1 = new Teacher { Name = "First", Surname = "Teacher" };
            var testTeacher2 = new Teacher { Name = "Second", Surname = "Teacher" };
            var testGroup1 = new Group { Name = "TestGrp-01", Course = testCourse1, Teacher = testTeacher1 };
            var testGroup2 = new Group { Name = "TestGrp-02", Course = testCourse1, Teacher = testTeacher1 };
            var testGroup3 = new Group { Name = "TestGrp-03", Course = testCourse1, Teacher = testTeacher2 };
            var testGroup4 = new Group { Name = "TestGrp-04", Course = testCourse1, Teacher = testTeacher2 };
            var testGroup5 = new Group { Name = "TestGrp-05", Course = testCourse1, Teacher = testTeacher2 };

            context.Courses.AddRange(testCourse1);
            context.Teachers.AddRange(testTeacher1, testTeacher2);
            context.Groups.AddRange(testGroup1, testGroup2, testGroup3, testGroup4, testGroup5);

            context.SaveChanges();
        }

        var expectedErrorMessage = "Teacher 'Teacher Third' not found!";

        try
        {
            var teacherNewName = "Second";
            var teacherNewSurname = "Teacher";
            var selectedTeacher = new TeacherDto { Name = "Teacher", Surname = "Third" };
            var updateTeacherService = new TeacherUpdateService(options);
            updateTeacherService.UpdateTeacher(teacherNewName, teacherNewSurname, selectedTeacher);

            Assert.Fail("Expected Exception was not thrown.");
        }
        catch (Exception actualError)
        {
            Assert.AreEqual(expectedErrorMessage, actualError.Message);
        }
    }
}
