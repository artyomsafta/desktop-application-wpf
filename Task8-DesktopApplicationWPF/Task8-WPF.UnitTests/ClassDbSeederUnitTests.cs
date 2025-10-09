using Microsoft.EntityFrameworkCore;
using Task8_WPF.DAL;
using Task8_WPF.DAL.Entities;

namespace Task8_WPF.UnitTests;

[TestClass]
public sealed class ClassDbSeederUnitTests
{
    [TestMethod]
    public void Test_Seed_EmptyDb()
    {
        var options = new DbContextOptionsBuilder<WpfAppDbContext>()
            .UseInMemoryDatabase(databaseName: "MockDb1Empty")
            .Options;

        using (var context = new WpfAppDbContext(options))
        {
            var emptyDbSeeder = new DbSeeder();
            emptyDbSeeder.Seed(context);

            Assert.AreEqual(5, context.Courses.Count());
            Assert.AreEqual(7, context.Teachers.Count());
            Assert.AreEqual(7, context.Groups.Count());
            Assert.AreEqual(76, context.Students.Count());
        }
    }

    [TestMethod]
    public void Test_Seed_DbWithData()
    {
        var options = new DbContextOptionsBuilder<WpfAppDbContext>()
            .UseInMemoryDatabase(databaseName: "MockDb32HasData")
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

            var dbWithDataSeeder = new DbSeeder();
            dbWithDataSeeder.Seed(context);

            Assert.AreEqual(1, context.Courses.Count());
            Assert.AreEqual(2, context.Teachers.Count());
            Assert.AreEqual(3, context.Groups.Count());
            Assert.AreEqual(9, context.Students.Count());
        }
    }
}
