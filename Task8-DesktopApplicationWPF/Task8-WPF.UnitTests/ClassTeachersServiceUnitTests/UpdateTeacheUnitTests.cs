using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.BAL.Services;
using Task8_WPF.DAL;
using Task8_WPF.DAL.Entities;

namespace Task8_WPF.UnitTests.ClassTeachersServiceUnitTests;

[TestClass]
public class UpdateTeacheUnitTests
{
    private DbContextOptions<WpfAppDbContext> _options;
    private TeachersService _teachersService;

    private static readonly Guid Course1Id = Guid.NewGuid();

    private static readonly Guid Teacher1Id = Guid.NewGuid();
    private static readonly Guid Teacher2Id = Guid.NewGuid();

    private static readonly Guid Group1Id = Guid.NewGuid();
    private static readonly Guid Group2Id = Guid.NewGuid();
    private static readonly Guid Group3Id = Guid.NewGuid();
    private static readonly Guid Group4Id = Guid.NewGuid();
    private static readonly Guid Group5Id = Guid.NewGuid();

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
            var testCourse1 = new Course { Id = Course1Id, Name = "Test course", Description = "This is test course" };
            var testTeacher1 = new Teacher { Id = Teacher1Id, Name = "First", Surname = "Teacher" };
            var testTeacher2 = new Teacher { Id = Teacher2Id, Name = "Teacher", Surname = "Third" };
            var testGroup1 = new Group { Id = Group1Id, Name = "TestGrp-01", Course = testCourse1, Teacher = testTeacher1 };
            var testGroup2 = new Group { Id = Group2Id, Name = "TestGrp-02", Course = testCourse1, Teacher = testTeacher1 };
            var testGroup3 = new Group { Id = Group3Id, Name = "TestGrp-03", Course = testCourse1, Teacher = testTeacher2 };
            var testGroup4 = new Group { Id = Group4Id, Name = "TestGrp-04", Course = testCourse1, Teacher = testTeacher2 };
            var testGroup5 = new Group { Id = Group5Id, Name = "TestGrp-05", Course = testCourse1, Teacher = testTeacher2 };

            context.Courses.AddRange(testCourse1);
            context.Teachers.AddRange(testTeacher1, testTeacher2);
            context.Groups.AddRange(testGroup1, testGroup2, testGroup3, testGroup4, testGroup5);

            context.SaveChanges();
        }
    }

    [TestMethod]
    public void Test_UpdateTeacher_PositiveCase()
    {
        using (var context = new WpfAppDbContext(_options))
        {
            var teacherNewName = "Second";
            var teacherNewSurname = "Teacher";
            var selectedTeacher = new TeacherDto { TeacherId = Teacher2Id, Name = "Teacher", Surname = "Third" };
            _teachersService.UpdateTeacher(teacherNewName, teacherNewSurname, selectedTeacher);

            var expectedTeacherValue = new TeacherDto { TeacherId = Teacher2Id, Name = "Second", Surname = "Teacher" };

            var actualTeacher = context.Teachers
                .FirstOrDefault(t => t.Id == expectedTeacherValue.TeacherId);

            var actualTeacherValue = new TeacherDto
            {
                TeacherId = actualTeacher.Id,
                Name = actualTeacher.Name,
                Surname = actualTeacher.Surname
            };

            actualTeacherValue.Should().BeEquivalentTo(expectedTeacherValue);
        }
    }

    [TestMethod]
    public void Test_UpdateTeacher_TeacherNotFoundCase()
    {
        var expectedErrorMessage = "Teacher 'WRONG Teacher' not found!";

        try
        {
            var teacherNewName = "Second";
            var teacherNewSurname = "Teacher";
            var selectedTeacher = new TeacherDto { TeacherId = Guid.NewGuid(), Name = "WRONG", Surname = "Teacher" };
            _teachersService.UpdateTeacher(teacherNewName, teacherNewSurname, selectedTeacher);

            Assert.Fail("Expected Exception was not thrown.");
        }
        catch (Exception actualError)
        {
            Assert.AreEqual(expectedErrorMessage, actualError.Message);
        }
    }
}
