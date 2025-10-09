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
    [TestMethod]
    public void Test_GetHierarchyForTreeView()
    {
        var options = new DbContextOptionsBuilder<WpfAppDbContext>()
            .UseInMemoryDatabase(databaseName: "MockDb33HasData")
            .Options;

        using (var context = new WpfAppDbContext(options))
        {
            var testCourse1 = new Course { Name = "Test course", Description = "This is test course" };
            var testTeacher1 = new Teacher { Name = "First", Surname = "Teacher" };
            var testTeacher2 = new Teacher { Name = "Second", Surname = "Teacher" };
            var testGroup1 = new Group { Name = "TestGrp-01", Course = testCourse1, Teacher = testTeacher1 };
            var testGroup2 = new Group { Name = "TestGrp-02", Course = testCourse1, Teacher = testTeacher1 };
            var testGroup3 = new Group { Name = "TestGrp-03", Course = testCourse1, Teacher = testTeacher2 };
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
                CourseName = "Test course",
                Groups = new List<GroupsTreeDto>
                {
                    new GroupsTreeDto
                    {
                        GroupName = "TestGrp-01",
                        Students = new List<StudentsTreeDto>
                        {
                            new StudentsTreeDto { FullName = "Lybov Pavlova" },
                            new StudentsTreeDto { FullName = "Talia Grimsley" }
                        }
                    },
                    new GroupsTreeDto
                    {
                        GroupName = "TestGrp-02",
                        Students = new List<StudentsTreeDto>
                        {
                            new StudentsTreeDto { FullName = "Artem Timchenko" },
                            new StudentsTreeDto { FullName = "Jaxon Moorland" },
                            new StudentsTreeDto { FullName = "Tessa Winsley" }
                        }
                    },
                    new GroupsTreeDto
                    {
                        GroupName = "TestGrp-03",
                        Students = new List<StudentsTreeDto>
                        {
                            new StudentsTreeDto { FullName = "Elara Mendez" },
                            new StudentsTreeDto { FullName = "Kian Halbrook" },
                            new StudentsTreeDto { FullName = "Caleb Raycroft" },
                            new StudentsTreeDto { FullName = "Milo Penrose" }
                        }
                    }
                }
            }
        };

        actualTreeViewData.Should().BeEquivalentTo(expectedTreeViewData);
    }
}
