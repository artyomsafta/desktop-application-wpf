using Microsoft.EntityFrameworkCore;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.BAL.Services.GroupsServices;
using Task8_WPF.DAL;
using Task8_WPF.DAL.Entities;

namespace Task8_WPF.UnitTests.GroupsServicesUnitTests;

[TestClass]
public class ClassGroupAddEntryServiceUnitTests
{
    [TestMethod]
    public void Test_AddGroupEntry_PositiveCase()
    {
        var options = new DbContextOptionsBuilder<WpfAppDbContext>()
            .UseInMemoryDatabase(databaseName: "MockDb6HasData")
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

            var newGroupName = "TestGrp-06";
            var selectedCourse = new CourseDto { CourseName = "Test course", Description = "This is test course" };
            var selectedTeacher = new TeacherDto { Name = "First", Surname = "Teacher" };
            var addGroupService = new GroupAddEntryService(newGroupName, selectedCourse, selectedTeacher, options);
            addGroupService.AddGroupEntry();

            Assert.AreEqual(6, context.Groups.Count());
        }
    }

    [TestMethod]
    public void Test_AddGroupEntry_NameTakenCase()
    {
        var options = new DbContextOptionsBuilder<WpfAppDbContext>()
            .UseInMemoryDatabase(databaseName: "MockDb7HasData")
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

            var expectedErrorMessage = "This group already exists! Try another name";

            try
            {
                var newGroupName = "TestGrp-05";
                var selectedCourse = new CourseDto { CourseName = "Test course", Description = "This is test course" };
                var selectedTeacher = new TeacherDto { Name = "First", Surname = "Teacher" };
                var newGroupEntry = new GroupAddEntryService(newGroupName, selectedCourse, selectedTeacher, options);
                newGroupEntry.AddGroupEntry();

                Assert.Fail("Expected Exception was not thrown.");
            }
            catch(Exception actualError)
            {
                Assert.AreEqual(expectedErrorMessage, actualError.Message);
            }
        }
    }

    [TestMethod]
    public void Test_AddGroupEntry_CourseNotFoundCase()
    {
        var options = new DbContextOptionsBuilder<WpfAppDbContext>()
            .UseInMemoryDatabase(databaseName: "MockDb8HasData")
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

            var expectedErrorMessage = "Course 'WRONG course' not found!";

            try
            {
                var newGroupName = "TestGrp-06";
                var selectedCourse = new CourseDto { CourseName = "WRONG course", Description = "This is WRONG course" };
                var selectedTeacher = new TeacherDto { Name = "First", Surname = "Teacher" };
                var newGroupEntry = new GroupAddEntryService(newGroupName, selectedCourse, selectedTeacher, options);
                newGroupEntry.AddGroupEntry();

                Assert.Fail("Expected Exception was not thrown.");
            }
            catch (Exception actualError)
            {
                Assert.AreEqual(expectedErrorMessage, actualError.Message);
            }
        }
    }

    [TestMethod]
    public void Test_AddGroupEntry_TeacherNotFoundCase()
    {
        var options = new DbContextOptionsBuilder<WpfAppDbContext>()
            .UseInMemoryDatabase(databaseName: "MockDb9HasData")
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

            var expectedErrorMessage = "Teacher 'WRONG Teacher' not found!";

            try
            {
                var newGroupName = "TestGrp-06";
                var selectedCourse = new CourseDto { CourseName = "Test course", Description = "This is test course" };
                var selectedTeacher = new TeacherDto { Name = "WRONG", Surname = "Teacher" };
                var newGroupEntry = new GroupAddEntryService(newGroupName, selectedCourse, selectedTeacher, options);
                newGroupEntry.AddGroupEntry();

                Assert.Fail("Expected Exception was not thrown.");
            }
            catch (Exception actualError)
            {
                Assert.AreEqual(expectedErrorMessage, actualError.Message);
            }
        }
    }
}
