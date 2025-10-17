using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.BAL.Services;
using Task8_WPF.DAL;
using Task8_WPF.DAL.Entities;

namespace Task8_WPF.UnitTests.ClassStudentsServiceUnitTests;

[TestClass]
public class UpdateStudenUnitTests
{
    private DbContextOptions<WpfAppDbContext> _options;
    private StudentsService _studentsService;

    private static readonly Guid Course1Id = Guid.NewGuid();

    private static readonly Guid Teacher1Id = Guid.NewGuid();
    private static readonly Guid Teacher2Id = Guid.NewGuid();

    private static readonly Guid Group1Id = Guid.NewGuid();
    private static readonly Guid Group2Id = Guid.NewGuid();
    private static readonly Guid Group3Id = Guid.NewGuid();
    private static readonly Guid Group4Id = Guid.NewGuid();
    private static readonly Guid Group5Id = Guid.NewGuid();

    private static readonly Guid Student1Group1Id = Guid.NewGuid();
    private static readonly Guid Student2Group1Id = Guid.NewGuid();
    private static readonly Guid Student1Group2Id = Guid.NewGuid();
    private static readonly Guid Student2Group2Id = Guid.NewGuid();
    private static readonly Guid Student3Group2Id = Guid.NewGuid();
    private static readonly Guid Student1Group3Id = Guid.NewGuid();
    private static readonly Guid Student2Group3Id = Guid.NewGuid();
    private static readonly Guid Student3Group3Id = Guid.NewGuid();
    private static readonly Guid Student4Group3Id = Guid.NewGuid();

    [TestInitialize]
    public void Setup()
    {
        _options = new DbContextOptionsBuilder<WpfAppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        this.SeedMockDb();
        _studentsService = new StudentsService(_options);
    }

    private void SeedMockDb()
    {
        using (var context = new WpfAppDbContext(_options))
        {
            var testCourse1 = new Course { Id = Course1Id, Name = "Test course", Description = "This is test course" };
            var testTeacher1 = new Teacher { Id = Teacher1Id, Name = "First", Surname = "Teacher" };
            var testTeacher2 = new Teacher { Id = Teacher2Id, Name = "Second", Surname = "Teacher" };
            var testGroup1 = new Group { Id = Group1Id, Name = "TestGrp-01", Course = testCourse1, Teacher = testTeacher1 };
            var testGroup2 = new Group { Id = Group2Id, Name = "TestGrp-02", Course = testCourse1, Teacher = testTeacher1 };
            var testGroup3 = new Group { Id = Group3Id, Name = "TestGrp-03", Course = testCourse1, Teacher = testTeacher2 };
            var testGroup4 = new Group { Id = Group4Id, Name = "TestGrp-04", Course = testCourse1, Teacher = testTeacher2 };
            var testGroup5 = new Group { Id = Group5Id, Name = "TestGrp-05", Course = testCourse1, Teacher = testTeacher2 };
            var testStudents = new List<Student>
            {
                new Student { Id = Student1Group1Id, Name = "Lybov", Surname = "Pavlova", Group = testGroup1 },
                new Student { Id = Student2Group1Id, Name = "Talia", Surname = "Grimsley", Group = testGroup1 },
                new Student { Id = Student1Group2Id, Name = "Artem", Surname = "Timchenko", Group = testGroup2 },
                new Student { Id = Student2Group2Id, Name = "Jaxon", Surname = "Moorland", Group = testGroup2 },
                new Student { Id = Student3Group2Id, Name = "Tessa", Surname = "Winsley", Group = testGroup2 },
                new Student { Id = Student1Group3Id, Name = "Elara", Surname = "Mendez", Group = testGroup3 },
                new Student { Id = Student2Group3Id, Name = "Kian", Surname = "Halbrook", Group = testGroup3 },
                new Student { Id = Student3Group3Id, Name = "Caleb", Surname = "Raycroft", Group = testGroup3 },
                new Student { Id = Student4Group3Id, Name = "Milo", Surname = "Penrose", Group = testGroup3 }
            };

            context.Courses.AddRange(testCourse1);
            context.Teachers.AddRange(testTeacher1, testTeacher2);
            context.Groups.AddRange(testGroup1, testGroup2, testGroup3, testGroup4, testGroup5);
            context.Students.AddRange(testStudents);

            context.SaveChanges();
        }
    }

    [TestMethod]
    public void Test_UpdateStudent_PositiveCase()
    {
        using (var context = new WpfAppDbContext(_options))
        {
            var studentNewName = "Bill";
            var studentNewSurname = "Gates";
            var selectedStudentId = Student2Group1Id;

            _studentsService.UpdateStudent(studentNewName, studentNewSurname, selectedStudentId);

            var expectedStudentValue = new StudentDto { StudentId = Student2Group1Id, Name = "Bill", Surname = "Gates", GroupName = "TestGrp-01" };

            var actualStudent = context.Students
                .Include(g => g.Group)
                .FirstOrDefault(s => s.Id == selectedStudentId);

            var actualStudentValue = new StudentDto
            {
                StudentId = actualStudent.Id,
                Name = actualStudent.Name,
                Surname = actualStudent.Surname,
                GroupName = actualStudent.Group.Name
            };

            actualStudentValue.Should().BeEquivalentTo(expectedStudentValue);
        }
    }

    [TestMethod]
    public void Test_UpdateStudent_StudentNotFoundCase()
    {
        var expectedErrorMessage = "Student not found!";

        try
        {
            var studentNewName = "Bill";
            var studentNewSurname = "Gates";
            var selectedStudentId = Guid.NewGuid();

            _studentsService.UpdateStudent(studentNewName, studentNewSurname, selectedStudentId);

            Assert.Fail("Expected Exception was not thrown.");
        }
        catch (Exception actualError)
        {
            Assert.AreEqual(expectedErrorMessage, actualError.Message);
        }
    }
}
