using Microsoft.EntityFrameworkCore;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.BAL.Services;
using Task8_WPF.DAL;
using Task8_WPF.DAL.Entities;

namespace Task8_WPF.UnitTests.ClassTeachersServiceUnitTests;

[TestClass]
public class DeleteTeacherUnitTests
{
    private DbContextOptions<WpfAppDbContext> _options;
    private TeachersService _teachersService;

    [TestInitialize]
    public void Setup()
    {
        _options = new DbContextOptionsBuilder<WpfAppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        this.SeedMockDb();
        _teachersService = new TeachersService(_options);
    }

    private void SeedMockDb()
    {
        using (var context = new WpfAppDbContext(_options))
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
        }
    }

    [TestMethod]
    public void Test_DeleteTeacher_PositiveCase()
    {
        using (var context = new WpfAppDbContext(_options))
        {
            var selectedTeacher = new TeacherDto { Name = "Third", Surname = "Teacher" };
            _teachersService.DeleteTeacher(selectedTeacher);

            Assert.AreEqual(2, context.Teachers.Count());
        }
    }

    [TestMethod]
    public void Test_DeleteTeacher_TeacherNotFoundCase()
    {
        var expectedErrorMessage = "Teacher 'WRONG Teacher' not found!";

        try
        {
            var selectedTeacher = new TeacherDto { Name = "WRONG", Surname = "Teacher" };
            _teachersService.DeleteTeacher(selectedTeacher);

            Assert.Fail("Expected Exception was not thrown.");
        }
        catch (Exception actualError)
        {
            Assert.AreEqual(expectedErrorMessage, actualError.Message);
        }       
    }

    [TestMethod]
    public void Test_DeleteTeacher_TeacherHasGroupsCase()
    {
        var expectedErrorMessage = "Teacher 'First Teacher' cannot be deleted because he still has groups!";

        try
        {
            var selectedTeacher = new TeacherDto { Name = "First", Surname = "Teacher" };
            _teachersService.DeleteTeacher(selectedTeacher);

            Assert.Fail("Expected Exception was not thrown.");
        }
        catch (Exception actualError)
        {
            Assert.AreEqual(expectedErrorMessage, actualError.Message);
        }       
    }
}
