using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.BAL.Services;
using Task8_WPF.DAL;
using Task8_WPF.DAL.Entities;

namespace Task8_WPF.UnitTests;

[TestClass]
public class DtoListsServiceUnitTests
{
    private DbContextOptions<WpfAppDbContext> _options;
    private DtoListsService _dtoListsService;

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
            var testCourse1 = new Course { Name = "Test course1", Description = "This is test course1" };
            var testCourse2 = new Course { Name = "Test course2", Description = "This is test course2" };
            var testCourse3 = new Course { Name = "Test course3", Description = "This is test course3" };

            var testTeacher1 = new Teacher { Name = "First", Surname = "Teacher" };
            var testTeacher2 = new Teacher { Name = "Second", Surname = "Teacher" };
            var testTeacher3 = new Teacher { Name = "Third", Surname = "Teacher" };

            var testGroup1 = new Group { Name = "TestGrp-01", Course = testCourse1, Teacher = testTeacher1 };
            var testGroup2 = new Group { Name = "TestGrp-02", Course = testCourse1, Teacher = testTeacher1 };
            var testGroup3 = new Group { Name = "TestGrp-03", Course = testCourse2, Teacher = testTeacher1 };
            var testGroup4 = new Group { Name = "TestGrp-04", Course = testCourse3, Teacher = testTeacher2 };
            var testGroup5 = new Group { Name = "TestGrp-05", Course = testCourse3, Teacher = testTeacher2 };

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

    [TestMethod]
    public void Test_GetGroupsList()
    {
        var actualGroupsListData = _dtoListsService.GetGroupsList();

        var expectedGroupsListData = new List<GroupDto>
        {
            new GroupDto
            {
                GroupName = "TestGrp-01",
                CourseName = "Test course1",
                TeacherFullName = "First Teacher"
            },
            new GroupDto
            {
                GroupName = "TestGrp-02",
                CourseName = "Test course1",
                TeacherFullName = "First Teacher"
            },
            new GroupDto
            {
                GroupName = "TestGrp-03",
                CourseName = "Test course2",
                TeacherFullName = "First Teacher"
            },
            new GroupDto
            {
                GroupName = "TestGrp-04",
                CourseName = "Test course3",
                TeacherFullName = "Second Teacher"
            },
            new GroupDto
            {
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
        var selectedGroupName = "TestGrp-03";
        var actualStudentsListData = _dtoListsService.GetStudentsList(selectedGroupName);

        var expectedStudentsListData = new List<StudentDto>
        {
            new StudentDto
            {
                Name = "Elara",
                Surname = "Mendez",
                GroupName = "TestGrp-03"
            },
            new StudentDto
            {
                Name = "Kian",
                Surname = "Halbrook",
                GroupName = "TestGrp-03"
            },
            new StudentDto
            {
                Name = "Caleb",
                Surname = "Raycroft",
                GroupName = "TestGrp-03"
            },
            new StudentDto
            {
                Name = "Milo",
                Surname = "Penrose",
                GroupName = "TestGrp-03"
            }
        };

        actualStudentsListData.Should().BeEquivalentTo(expectedStudentsListData);
    }

    [TestMethod]
    public void Test_GetTeachersList()
    {
        var actualTeachersListData = _dtoListsService.GetTeachersList();

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
