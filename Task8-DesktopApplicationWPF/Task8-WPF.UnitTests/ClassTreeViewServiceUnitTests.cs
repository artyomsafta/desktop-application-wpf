using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using Task8_WPF.DAL;
using Task8_WPF.DAL.Entities;
using Task8_WPF.BAL.Services;
using Task8_WPF.BAL.Dto.TreeDtos;

namespace Task8_WPF.UnitTests;

[TestClass]
public sealed class ClassTreeViewServiceUnitTests
{
    private static readonly Guid Course1Id = Guid.NewGuid();

    private static readonly Guid Teacher1Id = Guid.NewGuid();
    private static readonly Guid Teacher2Id = Guid.NewGuid();

    private static readonly Guid Group1Id = Guid.NewGuid();
    private static readonly Guid Group2Id = Guid.NewGuid();
    private static readonly Guid Group3Id = Guid.NewGuid();

    private static readonly Guid Student1Group1Id = Guid.NewGuid();
    private static readonly Guid Student2Group1Id = Guid.NewGuid();
    private static readonly Guid Student1Group2Id = Guid.NewGuid();
    private static readonly Guid Student2Group2Id = Guid.NewGuid();
    private static readonly Guid Student3Group2Id = Guid.NewGuid();
    private static readonly Guid Student1Group3Id = Guid.NewGuid();
    private static readonly Guid Student2Group3Id = Guid.NewGuid();
    private static readonly Guid Student3Group3Id = Guid.NewGuid();
    private static readonly Guid Student4Group3Id = Guid.NewGuid();

    [TestMethod]
    public void Test_GetHierarchyForTreeView()
    {
        var options = new DbContextOptionsBuilder<WpfAppDbContext>()
            .UseInMemoryDatabase(databaseName: "MockDb02HasData")
            .Options;

        using (var context = new WpfAppDbContext(options))
        {
            var testCourse1 = new Course { Id = Course1Id, Name = "Test course", Description = "This is test course" };
            var testTeacher1 = new Teacher { Id = Teacher1Id, Name = "First", Surname = "Teacher" };
            var testTeacher2 = new Teacher { Id = Teacher2Id, Name = "Second", Surname = "Teacher" };
            var testGroup1 = new Group { Id = Group1Id, Name = "TestGrp-01", Course = testCourse1, Teacher = testTeacher1 };
            var testGroup2 = new Group { Id = Group2Id, Name = "TestGrp-02", Course = testCourse1, Teacher = testTeacher1 };
            var testGroup3 = new Group { Id = Group3Id, Name = "TestGrp-03", Course = testCourse1, Teacher = testTeacher2 };
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
            context.Groups.AddRange(testGroup1, testGroup2, testGroup3);
            context.Students.AddRange(testStudents);

            context.SaveChanges();
        }

        var testTreeViewService = new TreeViewService(options);
        var actualTreeViewData = testTreeViewService.GetHierarchyForTreeView();

        var expectedTreeViewData = new List<CoursesTreeDto>
        {
            new CoursesTreeDto
            {
                CourseId = Course1Id,
                CourseName = "Test course",
                Groups = new List<GroupsTreeDto>
                {
                    new GroupsTreeDto
                    {
                        GroupId = Group1Id,
                        GroupName = "TestGrp-01",
                        Students = new List<StudentsTreeDto>
                        {
                            new StudentsTreeDto { StudentId = Student1Group1Id, FullName = "Lybov Pavlova" },
                            new StudentsTreeDto { StudentId = Student2Group1Id, FullName = "Talia Grimsley" }
                        }
                    },
                    new GroupsTreeDto
                    {
                        GroupId = Group2Id,
                        GroupName = "TestGrp-02",
                        Students = new List<StudentsTreeDto>
                        {
                            new StudentsTreeDto { StudentId = Student1Group2Id, FullName = "Artem Timchenko" },
                            new StudentsTreeDto { StudentId = Student2Group2Id, FullName = "Jaxon Moorland" },
                            new StudentsTreeDto { StudentId = Student3Group2Id, FullName = "Tessa Winsley" }
                        }
                    },
                    new GroupsTreeDto
                    {
                        GroupId = Group3Id,
                        GroupName = "TestGrp-03",
                        Students = new List<StudentsTreeDto>
                        {
                            new StudentsTreeDto { StudentId = Student1Group3Id, FullName = "Elara Mendez" },
                            new StudentsTreeDto { StudentId = Student2Group3Id, FullName = "Kian Halbrook" },
                            new StudentsTreeDto { StudentId = Student3Group3Id, FullName = "Caleb Raycroft" },
                            new StudentsTreeDto { StudentId = Student4Group3Id, FullName = "Milo Penrose" }
                        }
                    }
                }
            }
        };

        actualTreeViewData.Should().BeEquivalentTo(expectedTreeViewData);
    }
}
