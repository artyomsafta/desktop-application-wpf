using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.BAL.Services;
using Task8_WPF.DAL;
using Task8_WPF.DAL.Entities;

namespace Task8_WPF.UnitTests;

[TestClass]
public class ClassDtoListsServiceUnitTests
{
    private DbContextOptions<WpfAppDbContext> _options;
    private DtoListsService _dtoListsService;

    private static readonly Guid Course1Id = Guid.NewGuid();
    private static readonly Guid Course2Id = Guid.NewGuid();
    private static readonly Guid Course3Id = Guid.NewGuid();

    private static readonly Guid Teacher1Id = Guid.NewGuid();
    private static readonly Guid Teacher2Id = Guid.NewGuid();
    private static readonly Guid Teacher3Id = Guid.NewGuid();

    private static readonly Guid Group1Id = Guid.NewGuid();
    private static readonly Guid Group2Id = Guid.NewGuid();
    private static readonly Guid Group3Id = Guid.NewGuid();
    private static readonly Guid Group4Id = Guid.NewGuid();
    private static readonly Guid Group5Id = Guid.NewGuid();

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
        _dtoListsService = new DtoListsService(_options);
    }

    private void SeedMockDb()
    {
        using (var context = new WpfAppDbContext(_options))
        {
            var testCourse1 = new Course { Id = Course1Id, Name = "Test course1", Description = "This is test course1" };
            var testCourse2 = new Course { Id = Course2Id, Name = "Test course2", Description = "This is test course2" };
            var testCourse3 = new Course { Id = Course3Id, Name = "Test course3", Description = "This is test course3" };

            var testTeacher1 = new Teacher { Id = Teacher1Id, Name = "First", Surname = "Teacher" };
            var testTeacher2 = new Teacher { Id = Teacher2Id, Name = "Second", Surname = "Teacher" };
            var testTeacher3 = new Teacher { Id = Teacher3Id, Name = "Third", Surname = "Teacher" };

            var testGroup1 = new Group { Id = Group1Id, Name = "TestGrp-01", Course = testCourse1, Teacher = testTeacher1 };
            var testGroup2 = new Group { Id = Group2Id, Name = "TestGrp-02", Course = testCourse1, Teacher = testTeacher1 };
            var testGroup3 = new Group { Id = Group3Id, Name = "TestGrp-03", Course = testCourse2, Teacher = testTeacher1 };
            var testGroup4 = new Group { Id = Group4Id, Name = "TestGrp-04", Course = testCourse3, Teacher = testTeacher2 };
            var testGroup5 = new Group { Id = Group5Id, Name = "TestGrp-05", Course = testCourse3, Teacher = testTeacher2 };

            var testStudents = new List<Student>
            {
                new Student { Name = "Lybov", Surname = "Pavlova", Group = testGroup1 },
                new Student { Name = "Talia", Surname = "Grimsley", Group = testGroup1 },
                new Student { Name = "Artem", Surname = "Timchenko", Group = testGroup2 },
                new Student { Name = "Jaxon", Surname = "Moorland", Group = testGroup2 },
                new Student { Name = "Tessa", Surname = "Winsley", Group = testGroup2 },
                new Student { Id = Student1Group3Id, Name = "Elara", Surname = "Mendez", Group = testGroup3 },
                new Student { Id = Student2Group3Id, Name = "Kian", Surname = "Halbrook", Group = testGroup3 },
                new Student { Id = Student3Group3Id, Name = "Caleb", Surname = "Raycroft", Group = testGroup3 },
                new Student { Id = Student4Group3Id, Name = "Milo", Surname = "Penrose", Group = testGroup3 }
            };


            context.Courses.AddRange(testCourse1, testCourse2, testCourse3);
            context.Teachers.AddRange(testTeacher1, testTeacher2, testTeacher3);
            context.Groups.AddRange(testGroup1, testGroup2, testGroup3, testGroup4, testGroup5);
            context.Students.AddRange(testStudents);

            context.SaveChanges();
        }
    }

    [TestMethod]
    public void Test_GetCoursesList()
    {
        var actualCoursesListData = _dtoListsService.GetCoursesList();

        var expectedCoursesListData = new List<CourseDto>
        {
            new CourseDto
            {
                CourseId = Course1Id,
                CourseName = "Test course1",
                Description = "This is test course1"
            },
            new CourseDto
            {
                CourseId= Course2Id,
                CourseName = "Test course2",
                Description = "This is test course2"
            },
            new CourseDto
            {
                CourseId = Course3Id,
                CourseName = "Test course3",
                Description = "This is test course3"
            }
        };

        actualCoursesListData.Should().BeEquivalentTo(expectedCoursesListData);
    }

    [TestMethod]
    public void Test_GetGroupsList()
    {
        var actualGroupsListData = _dtoListsService.GetGroupsList();

        var expectedGroupsListData = new List<GroupDto>
        {
            new GroupDto
            {
                GroupId = Group1Id,
                GroupName = "TestGrp-01",
                CourseName = "Test course1",
                TeacherFullName = "First Teacher"
            },
            new GroupDto
            {
                GroupId = Group2Id,
                GroupName = "TestGrp-02",
                CourseName = "Test course1",
                TeacherFullName = "First Teacher"
            },
            new GroupDto
            {
                GroupId = Group3Id,
                GroupName = "TestGrp-03",
                CourseName = "Test course2",
                TeacherFullName = "First Teacher"
            },
            new GroupDto
            {
                GroupId = Group4Id,
                GroupName = "TestGrp-04",
                CourseName = "Test course3",
                TeacherFullName = "Second Teacher"
            },
            new GroupDto
            {
                GroupId = Group5Id,
                GroupName = "TestGrp-05",
                CourseName = "Test course3",
                TeacherFullName = "Second Teacher"
            }
        };

        actualGroupsListData.Should().BeEquivalentTo(expectedGroupsListData);
    }

    [TestMethod]
    public void Test_GetStudentsList()
    {
        using (var context = new WpfAppDbContext(_options))
        {
            var selectedGroup = context.Groups
                .Include(c => c.Course)
                .Include(t => t.Teacher)
                .Include(s => s.Students)
                .Where(g => g.Name == "TestGrp-03")
                .FirstOrDefault();

            var selectedGroupDto = new GroupDto
            {
                GroupId = selectedGroup.Id,
                GroupName = selectedGroup.Name,
                CourseName = selectedGroup.Course.Name,
                TeacherFullName = selectedGroup.Teacher.Name + " " + selectedGroup.Teacher.Surname
            };

            var actualStudentsListData = _dtoListsService.GetStudentsList(selectedGroupDto);

            var expectedStudentsListData = new List<StudentDto>
            {
                new StudentDto
                {
                    StudentId = Student1Group3Id,
                    Name = "Elara",
                    Surname = "Mendez",
                    GroupName = "TestGrp-03"
                },
                new StudentDto
                {
                    StudentId = Student2Group3Id,
                    Name = "Kian",
                    Surname = "Halbrook",
                    GroupName = "TestGrp-03"
                },
                new StudentDto
                {
                    StudentId = Student3Group3Id,
                    Name = "Caleb",
                    Surname = "Raycroft",
                    GroupName = "TestGrp-03"
                },
                new StudentDto
                {
                    StudentId = Student4Group3Id,
                    Name = "Milo",
                    Surname = "Penrose",
                    GroupName = "TestGrp-03"
                }
            };

            actualStudentsListData.Should().BeEquivalentTo(expectedStudentsListData);
        }
    }

    [TestMethod]
    public void Test_GetTeachersList()
    {
        var actualTeachersListData = _dtoListsService.GetTeachersList();

        var expectedTeachersListData = new List<TeacherDto>
        {
            new TeacherDto
            {
                TeacherId = Teacher1Id,
                Name = "First",
                Surname = "Teacher"
            },
            new TeacherDto
            {
                TeacherId = Teacher2Id,
                Name = "Second",
                Surname = "Teacher"
            },
            new TeacherDto
            {
                TeacherId = Teacher3Id,
                Name = "Third",
                Surname = "Teacher"
            }
        };

        actualTeachersListData.Should().BeEquivalentTo(expectedTeachersListData);
    }
}
