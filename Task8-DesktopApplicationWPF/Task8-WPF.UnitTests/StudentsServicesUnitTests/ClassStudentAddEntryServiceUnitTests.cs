using Microsoft.EntityFrameworkCore;
using Task8_WPF.BAL.Services.StudentsServices;
using Task8_WPF.DAL;
using Task8_WPF.DAL.Entities;

namespace Task8_WPF.UnitTests.StudentsServicesUnitTests;

[TestClass]
public class ClassStudentAddEntryServiceUnitTests
{
    [TestMethod]
    public void Test_AddStudentEntry_PositiveCase()
    {
        var options = new DbContextOptionsBuilder<WpfAppDbContext>()
            .UseInMemoryDatabase(databaseName: "MockDb18HasData")
            .Options;

        using (var context = new WpfAppDbContext(options))
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

            var newStudentName = "Test";
            var newStudentSurname = "Student";
            var selectedGroupName = "TestGrp-01";

            var addStudentService = new StudentAddEntryService(options);
            addStudentService.AddStudentEntry(newStudentName, newStudentSurname, selectedGroupName);

            Assert.AreEqual(3, context.Students
                                .Include(g => g.Group)
                                .Where(g => g.Group.Name == selectedGroupName)
                                .Count()
            );
        }
    }

    [TestMethod]
    public void Test_AddStudentEntry_GroupNotFoundCase()
    {
        var options = new DbContextOptionsBuilder<WpfAppDbContext>()
            .UseInMemoryDatabase(databaseName: "MockDb19HasData")
            .Options;

        using (var context = new WpfAppDbContext(options))
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

            var newStudentName = "Test";
            var newStudentSurname = "Student";
            var selectedGroupName = "TestGrp-06";

            var expectedErrorMessage = "Group 'TestGrp-06' not found!";

            try
            {
                var addStudentService = new StudentAddEntryService(options);
                addStudentService.AddStudentEntry(newStudentName, newStudentSurname, selectedGroupName);

                Assert.Fail("Expected Exception was not thrown.");
            }
            catch (Exception actualError)
            {
                Assert.AreEqual(expectedErrorMessage, actualError.Message);
            }
        }
    }
}
