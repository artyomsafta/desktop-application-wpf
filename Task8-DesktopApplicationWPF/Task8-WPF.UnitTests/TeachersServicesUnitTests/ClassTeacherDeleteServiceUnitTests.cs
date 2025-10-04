using Microsoft.EntityFrameworkCore;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.BAL.Services.TeachersServices;
using Task8_WPF.DAL;
using Task8_WPF.DAL.Entities;

namespace Task8_WPF.UnitTests.TeachersServicesUnitTests;

[TestClass]
public class ClassTeacherDeleteServiceUnitTests
{
    [TestMethod]
    public void Test_DeleteTeacher_PositiveCase()
    {
        var options = new DbContextOptionsBuilder<WpfAppDbContext>()
            .UseInMemoryDatabase(databaseName: "MockDb23HasData")
            .Options;

        using (var context = new WpfAppDbContext(options))
        {
            var testCourse1 = new Course { Name = "Test course", Description = "This is test course" };
            var testTeacher1 = new Teacher { Name = "First", Surname = "Teacher" };
            var testTeacher2 = new Teacher { Name = "Second", Surname = "Teacher" };
            var testTeacher3 = new Teacher { Name = "Third", Surname = "Teacher" };
            var testGroup1 = new Group { Name = "TestGrp-01", Course = testCourse1, Teacher = testTeacher1 };
            var testGroup2 = new Group { Name = "TestGrp-02", Course = testCourse1, Teacher = testTeacher1 };
            var testGroup3 = new Group { Name = "TestGrp-03", Course = testCourse1, Teacher = testTeacher2 };
            var testGroup4 = new Group { Name = "TestGrp-04", Course = testCourse1, Teacher = testTeacher2 };
            var testGroup5 = new Group { Name = "TestGrp-05", Course = testCourse1, Teacher = testTeacher2 };

            context.Courses.AddRange(testCourse1);
            context.Teachers.AddRange(testTeacher1, testTeacher2, testTeacher3);
            context.Groups.AddRange(testGroup1, testGroup2, testGroup3, testGroup4, testGroup5);

            context.SaveChanges();

            var selectedTeacher = new TeacherDto { Name = "Third", Surname = "Teacher" };
            var deleteTeacherService = new TeacherDeleteService(selectedTeacher, options);
            deleteTeacherService.DeleteTeacher();

            Assert.AreEqual(2, context.Teachers.Count());
        }
    }

    [TestMethod]
    public void Test_DeleteTeacher_TeacherNotFoundCase()
    {
        var options = new DbContextOptionsBuilder<WpfAppDbContext>()
            .UseInMemoryDatabase(databaseName: "MockDb24HasData")
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

            var expectedErrorMessage = "Teacher 'Third Teacher' not found!";

            try
            {
                var selectedTeacher = new TeacherDto { Name = "Third", Surname = "Teacher" };
                var deleteTeacherService = new TeacherDeleteService(selectedTeacher, options);
                deleteTeacherService.DeleteTeacher();

                Assert.Fail("Expected Exception was not thrown.");
            }
            catch (Exception actualError)
            {
                Assert.AreEqual(expectedErrorMessage, actualError.Message);
            }
        }
    }

    [TestMethod]
    public void Test_DeleteTeacher_TeacherHasGroupsCase()
    {
        var options = new DbContextOptionsBuilder<WpfAppDbContext>()
            .UseInMemoryDatabase(databaseName: "MockDb25HasData")
            .Options;

        using (var context = new WpfAppDbContext(options))
        {
            var testCourse1 = new Course { Name = "Test course", Description = "This is test course" };
            var testTeacher1 = new Teacher { Name = "First", Surname = "Teacher" };
            var testTeacher2 = new Teacher { Name = "Second", Surname = "Teacher" };
            var testTeacher3 = new Teacher { Name = "Third", Surname = "Teacher" };
            var testGroup1 = new Group { Name = "TestGrp-01", Course = testCourse1, Teacher = testTeacher1 };
            var testGroup2 = new Group { Name = "TestGrp-02", Course = testCourse1, Teacher = testTeacher1 };
            var testGroup3 = new Group { Name = "TestGrp-03", Course = testCourse1, Teacher = testTeacher2 };
            var testGroup4 = new Group { Name = "TestGrp-04", Course = testCourse1, Teacher = testTeacher2 };
            var testGroup5 = new Group { Name = "TestGrp-05", Course = testCourse1, Teacher = testTeacher2 };
            var testGroup6 = new Group { Name = "TestGrp-06", Course = testCourse1, Teacher = testTeacher3 };

            context.Courses.AddRange(testCourse1);
            context.Teachers.AddRange(testTeacher1, testTeacher2, testTeacher3);
            context.Groups.AddRange(testGroup1, testGroup2, testGroup3, testGroup4, testGroup5, testGroup6);

            context.SaveChanges();

            var expectedErrorMessage = "Teacher 'Third Teacher' cannot be deleted because he still has groups!";

            try
            {
                var selectedTeacher = new TeacherDto { Name = "Third", Surname = "Teacher" };
                var deleteTeacherService = new TeacherDeleteService(selectedTeacher, options);
                deleteTeacherService.DeleteTeacher();

                Assert.Fail("Expected Exception was not thrown.");
            }
            catch (Exception actualError)
            {
                Assert.AreEqual(expectedErrorMessage, actualError.Message);
            }
        }
    }
}
