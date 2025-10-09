using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.BAL.Services.DtoListsServices;
using Task8_WPF.DAL;
using Task8_WPF.DAL.Entities;

namespace Task8_WPF.UnitTests.DtoListsServicesUnitTests;

[TestClass]
public class ClassCoursesListServiceUnitTests
{
    [TestMethod]
    public void Test_GetCoursesList()
    {
        var options = new DbContextOptionsBuilder<WpfAppDbContext>()
            .UseInMemoryDatabase(databaseName: "MockDb1HasData")
            .Options;

        using (var context = new WpfAppDbContext(options))
        {
            var testCourse1 = new Course { Name = "Test course1", Description = "This is test course1" };
            var testCourse2 = new Course { Name = "Test course2", Description = "This is test course2" };
            var testCourse3 = new Course { Name = "Test course3", Description = "This is test course3" };

            context.Courses.AddRange(testCourse1, testCourse2, testCourse3);
            context.SaveChanges();
        }

        var testCoursesListService = new CoursesListService(options);
        var actualCoursesListData = testCoursesListService.GetCoursesList();

        var expectedCoursesListData = new List<CourseDto>
        {
            new CourseDto
            {
                CourseName = "Test course1",
                Description = "This is test course1"
            },
            new CourseDto
            {
                CourseName = "Test course2",
                Description = "This is test course2"
            },
            new CourseDto
            {
                CourseName = "Test course3",
                Description = "This is test course3"
            }
        };

        actualCoursesListData.Should().BeEquivalentTo(expectedCoursesListData);
    }
}
