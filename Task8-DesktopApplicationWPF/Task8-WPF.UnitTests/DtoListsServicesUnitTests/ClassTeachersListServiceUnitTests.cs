using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.BAL.Services.DtoListsServices;
using Task8_WPF.DAL;
using Task8_WPF.DAL.Entities;

namespace Task8_WPF.UnitTests.DtoListsServicesUnitTests;

[TestClass]
public class ClassTeachersListServiceUnitTests
{
    [TestMethod]
    public void Test_GetTeachersList()
    {
        var options = new DbContextOptionsBuilder<WpfAppDbContext>()
            .UseInMemoryDatabase(databaseName: "MockDb5HasData")
            .Options;

        using (var context = new WpfAppDbContext(options))
        {
            var testTeacher1 = new Teacher { Name = "First", Surname = "Teacher" };
            var testTeacher2 = new Teacher { Name = "Second", Surname = "Teacher" };
            var testTeacher3 = new Teacher { Name = "Third", Surname = "Teacher" };

            context.Teachers.AddRange(testTeacher1, testTeacher2, testTeacher3);
            context.SaveChanges();
        }

        var testTeachersListService = new TeachersListService(options);
        var actualTeachersListData = testTeachersListService.GetTeachersList();

        var expectedTeachersListData = new List<TeacherDto>
        {
            new TeacherDto
            {
                Name = "First",
                Surname = "Teacher"
            },
            new TeacherDto
            {
                Name = "Second",
                Surname = "Teacher"
            },
            new TeacherDto
            {
                Name = "Third",
                Surname = "Teacher"
            }
        };

        actualTeachersListData.Should().BeEquivalentTo(expectedTeachersListData);
    }
}
