using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.BAL.Services.DtoListsServices;
using Task8_WPF.DAL;
using Task8_WPF.DAL.Entities;

namespace Task8_WPF.UnitTests.DtoListsServicesUnitTests;

[TestClass]
public class ClassGroupsListServiceUnitTests
{
    [TestMethod]
    public void Test_GetGroupsList()
    {
        var options = new DbContextOptionsBuilder<WpfAppDbContext>()
            .UseInMemoryDatabase(databaseName: "MockDb4HasData")
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

        var testGroupsListService = new GroupsListService(options);
        var actualGroupsListData = testGroupsListService.GetGroupsList();

        var expectedGroupsListData = new List<GroupDto>
        {
            new GroupDto
            {
                GroupName = "TestGrp-01",
                CourseName = "Test course",
                TeacherFullName = "First Teacher"
            },
            new GroupDto
            {
                GroupName = "TestGrp-02",
                CourseName = "Test course",
                TeacherFullName = "First Teacher"
            },
            new GroupDto
            {
                GroupName = "TestGrp-03",
                CourseName = "Test course",
                TeacherFullName = "Second Teacher"
            },
            new GroupDto
            {
                GroupName = "TestGrp-04",
                CourseName = "Test course",
                TeacherFullName = "Second Teacher"
            },
            new GroupDto
            {
                GroupName = "TestGrp-05",
                CourseName = "Test course",
                TeacherFullName = "Second Teacher"
            }
        };

        actualGroupsListData.Should().BeEquivalentTo(expectedGroupsListData);
    }
}
