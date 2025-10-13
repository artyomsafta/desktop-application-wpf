using Microsoft.EntityFrameworkCore;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.BAL.Services;
using Task8_WPF.DAL;
using Task8_WPF.DAL.Entities;

namespace Task8_WPF.UnitTests.ClassGroupsServiceUnitTests;

[TestClass]
public class DeleteGroupUnitTests
{
    private DbContextOptions<WpfAppDbContext> _options;
    private GroupsService _groupsService;

    [TestInitialize]
    public void Setup()
    {
        _options = new DbContextOptionsBuilder<WpfAppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        this.SeedMockDb();
        _groupsService = new GroupsService(_options);
    }

    private void SeedMockDb()
    {
        using (var context = new WpfAppDbContext(_options))
        {
            var testCourse1 = new Course { Name = "Test course", Description = "This is test course" };
            var testTeacher1 = new Teacher { Name = "First", Surname = "Teacher" };
            var testTeacher2 = new Teacher { Name = "Second", Surname = "Teacher" };
            var testGroup1 = new Group { Name = "TestGrp-01", Course = testCourse1, Teacher = testTeacher1 };
            var testGroup2 = new Group { Name = "TestGrp-02", Course = testCourse1, Teacher = testTeacher1 };
            var testGroup3 = new Group { Name = "TestGrp-03", Course = testCourse1, Teacher = testTeacher2 };
            var testGroup4 = new Group { Name = "TestGrp-04", Course = testCourse1, Teacher = testTeacher2 };
            var testGroup5 = new Group { Name = "TestGrp-05", Course = testCourse1, Teacher = testTeacher2 };

            var testStudents = new List<Student>
            {
                new Student { Name = "Lybov", Surname = "Pavlova", Group = testGroup1 },
                new Student { Name = "Talia", Surname = "Grimsley", Group = testGroup1 },
                new Student { Name = "Artem", Surname = "Timchenko", Group = testGroup2 },
                new Student { Name = "Jaxon", Surname = "Moorland", Group = testGroup2 },
                new Student { Name = "Tessa", Surname = "Winsley", Group = testGroup2 },
                new Student { Name = "Elara", Surname = "Mendez", Group = testGroup3 },
                new Student { Name = "Kian", Surname = "Halbrook", Group = testGroup3 },
                new Student { Name = "Caleb", Surname = "Raycroft", Group = testGroup3 },
                new Student { Name = "Milo", Surname = "Penrose", Group = testGroup3 }
            };

            context.Courses.AddRange(testCourse1);
            context.Teachers.AddRange(testTeacher1, testTeacher2);
            context.Groups.AddRange(testGroup1, testGroup2, testGroup3, testGroup4, testGroup5);
            context.Students.AddRange(testStudents);

            context.SaveChanges();
        }
    }

    [TestMethod]
    public void Test_DeleteGroup_PositiveCase()
    {
        using (var context = new WpfAppDbContext(_options))
        {
            var selectedGroup = new GroupDto { CourseName = "Test course", GroupName = "TestGrp-05", TeacherFullName = "Second Teacher" };
            _groupsService.DeleteGroup(selectedGroup);

            Assert.AreEqual(4, context.Groups.Count());
        }
    }

    [TestMethod]
    public void Test_DeleteGroup_GroupNotFoundCase()
    {
        var expectedErrorMessage = "Group TestGrp-06 not found!";

        using (var context = new WpfAppDbContext(_options))
        {
            try
            {
                var selectedGroup = new GroupDto { CourseName = "Test course", GroupName = "TestGrp-06", TeacherFullName = "Second Teacher" };
                _groupsService.DeleteGroup(selectedGroup);

                Assert.Fail("Expected Exception was not thrown.");
            }
            catch (Exception actualError)
            {
                Assert.AreEqual(expectedErrorMessage, actualError.Message);
            }
        }
    }

    [TestMethod]
    public void Test_DeleteGroup_GroupNotEmptyCase()
    {
        var expectedErrorMessage = "Group TestGrp-01 cannot be deleted because it has students in it!";

        using (var context = new WpfAppDbContext(_options))
        {
            try
            {
                var selectedGroup = new GroupDto { CourseName = "Test course", GroupName = "TestGrp-01", TeacherFullName = "First Teacher" };
                _groupsService.DeleteGroup(selectedGroup);

                Assert.Fail("Expected Exception was not thrown.");
            }
            catch (Exception actualError)
            {
                Assert.AreEqual(expectedErrorMessage, actualError.Message);
            }
        }
    }
}
