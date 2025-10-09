using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.BAL.Services.GroupsServices;
using Task8_WPF.DAL;
using Task8_WPF.DAL.Entities;

namespace Task8_WPF.UnitTests.GroupsServicesUnitTests;

[TestClass]
public class ClassGroupRenameServiceUnitTests
{
    [TestMethod]
    public void Test_RenameGroup_PositiveCase()
    {
        var options = new DbContextOptionsBuilder<WpfAppDbContext>()
            .UseInMemoryDatabase(databaseName: "MockDb12HasData")
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
            var testGroup5 = new Group { Name = "TestGrp-06", Course = testCourse1, Teacher = testTeacher2 };

            context.Courses.AddRange(testCourse1);
            context.Teachers.AddRange(testTeacher1, testTeacher2);
            context.Groups.AddRange(testGroup1, testGroup2, testGroup3, testGroup4, testGroup5);

            context.SaveChanges();
        }

        using (var context = new WpfAppDbContext(options))
        {
            var groupNewName = "TestGrp-05";
            var selectedGroup = new GroupDto { CourseName = "Test course", GroupName = "TestGrp-06", TeacherFullName = "Second Teacher" };
            var renameGroupService = new GroupRenameService(options);
            renameGroupService.RenameGroup(groupNewName, selectedGroup);

            var expectedGroupValue = new GroupDto { CourseName = "Test course", GroupName = "TestGrp-05", TeacherFullName = "Second Teacher" };

            var actualGroup = context.Groups
                .Include(c => c.Course)
                .Include(t => t.Teacher)
                .FirstOrDefault(g => g.Name == groupNewName);

            var actualGroupValue = new GroupDto
            {
                CourseName = actualGroup.Course.Name,
                GroupName = actualGroup.Name,
                TeacherFullName = actualGroup.Teacher.Name + " " + actualGroup.Teacher.Surname
            };

            actualGroupValue.Should().BeEquivalentTo(expectedGroupValue);
        }
    }

    [TestMethod]
    public void Test_RenameGroup_NameTakenCase()
    {
        var options = new DbContextOptionsBuilder<WpfAppDbContext>()
            .UseInMemoryDatabase(databaseName: "MockDb13HasData")
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

        var expectedErrorMessage = "This group already exists! Try another name";

        try
        {
            using (var context = new WpfAppDbContext(options))
            {
                var groupNewName = "TestGrp-01";
                var selectedGroup = new GroupDto { CourseName = "Test course", GroupName = "TestGrp-05", TeacherFullName = "Second Teacher" };
                var renameGroupService = new GroupRenameService(options);
                renameGroupService.RenameGroup(groupNewName, selectedGroup);

                Assert.Fail("Expected Exception was not thrown.");
            }
        }
        catch (Exception actualError)
        {
            Assert.AreEqual(expectedErrorMessage, actualError.Message);
        }
    }

    [TestMethod]
    public void Test_RenameGroup_GroupNotFoundCase()
    {
        var options = new DbContextOptionsBuilder<WpfAppDbContext>()
            .UseInMemoryDatabase(databaseName: "MockDb14HasData")
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

        var expectedErrorMessage = "Group TestGrp-06 not found!";

        try
        {
            using (var context = new WpfAppDbContext(options))
            {
                var groupNewName = "TestGrp-07";
                var selectedGroup = new GroupDto { CourseName = "Test course", GroupName = "TestGrp-06", TeacherFullName = "Second Teacher" };
                var renameGroupService = new GroupRenameService(options);
                renameGroupService.RenameGroup(groupNewName, selectedGroup);

                Assert.Fail("Expected Exception was not thrown.");
            }
        }
        catch (Exception actualError)
        {
            Assert.AreEqual(expectedErrorMessage, actualError.Message);
        }
    }
}
