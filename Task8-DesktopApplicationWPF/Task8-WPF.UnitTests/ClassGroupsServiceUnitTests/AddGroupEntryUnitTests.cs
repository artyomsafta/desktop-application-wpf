using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.BAL.Services;
using Task8_WPF.DAL;
using Task8_WPF.DAL.Entities;

namespace Task8_WPF.UnitTests.ClassGroupsServiceUnitTests;

[TestClass]
public class AddGroupEntryUnitTests
{
    private DbContextOptions<WpfAppDbContext> _options;
    private GroupsService _groupsService;

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
        _groupsService = new GroupsService(_options);
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

            context.Courses.AddRange(testCourse1);
            context.Teachers.AddRange(testTeacher1, testTeacher2);
            context.Groups.AddRange(testGroup1, testGroup2, testGroup3, testGroup4, testGroup5);

            context.SaveChanges();
        }
    }

    [TestMethod]
    public void Test_AddGroupEntry_PositiveCase()
    {
        var newGroupName = "TestGrp-06";
        var selectedCourse = new CourseDto { CourseId = Course1Id, CourseName = "Test course", Description = "This is test course" };
        var selectedTeacher = new TeacherDto { TeacherId = Teacher1Id, Name = "First", Surname = "Teacher" };

        using (var context = new WpfAppDbContext(_options))
        {
            _groupsService.AddGroupEntry(newGroupName, selectedCourse, selectedTeacher);
            Assert.AreEqual(6, context.Groups.Count());
        }
    }

    [TestMethod]
    public void Test_AddGroupEntry_PositiveCaseWithSpaces()
    {
        var newGroupName = "   TestGrp-06    ";
        var selectedCourse = new CourseDto { CourseId = Course1Id, CourseName = "Test course", Description = "This is test course" };
        var selectedTeacher = new TeacherDto { TeacherId = Teacher1Id, Name = "First", Surname = "Teacher" };

        using (var context = new WpfAppDbContext(_options))
        {
            _groupsService.AddGroupEntry(newGroupName, selectedCourse, selectedTeacher);
            Assert.AreEqual(6, context.Groups.Count());

            var expectedGroupValue = new GroupDto { GroupId = Guid.Empty, CourseName = "Test course", GroupName = "TestGrp-06", TeacherFullName = "First Teacher" };
            var actualGroup = context.Groups
                .Include(c => c.Course)
                .Include(t => t.Teacher)
                .FirstOrDefault(g => g.Name == expectedGroupValue.GroupName);
            var actualGroupValue = new GroupDto
            {
                GroupId = Guid.Empty,
                CourseName = actualGroup.Course.Name,
                GroupName = actualGroup.Name,
                TeacherFullName = actualGroup.Teacher.Name + " " + actualGroup.Teacher.Surname
            };

            actualGroupValue.Should().BeEquivalentTo(expectedGroupValue);
        }
    }

    [TestMethod]
    public void Test_AddGroupEntry_NameTakenCase()
    {
        var expectedErrorMessage = "This group already exists! Try another name";

        try
        {
            var newGroupName = "TestGrp-05";
            var selectedCourse = new CourseDto { CourseId = Course1Id, CourseName = "Test course", Description = "This is test course" };
            var selectedTeacher = new TeacherDto { TeacherId = Teacher1Id, Name = "First", Surname = "Teacher" };
            _groupsService.AddGroupEntry(newGroupName, selectedCourse, selectedTeacher);

            Assert.Fail("Expected Exception was not thrown.");
        }
        catch (Exception actualError)
        {
            Assert.AreEqual(expectedErrorMessage, actualError.Message);
        }
    }

    [TestMethod]
    public void Test_AddGroupEntry_NameTakenCaseWithRegisterDiff()
    {
        var expectedErrorMessage = "This group already exists! Try another name";

        try
        {
            var newGroupName = "TESTGRP-05";
            var selectedCourse = new CourseDto { CourseId = Course1Id, CourseName = "Test course", Description = "This is test course" };
            var selectedTeacher = new TeacherDto { TeacherId = Teacher1Id, Name = "First", Surname = "Teacher" };
            _groupsService.AddGroupEntry(newGroupName, selectedCourse, selectedTeacher);

            Assert.Fail("Expected Exception was not thrown.");
        }
        catch (Exception actualError)
        {
            Assert.AreEqual(expectedErrorMessage, actualError.Message);
        }
    }

    [TestMethod]
    public void Test_AddGroupEntry_CourseNotFoundCase()
    {
        var expectedErrorMessage = "Course 'WRONG course' not found!";

        try
        {
            var newGroupName = "TestGrp-06";
            var selectedCourse = new CourseDto { CourseId = Guid.NewGuid(), CourseName = "WRONG course", Description = "This is WRONG course" };
            var selectedTeacher = new TeacherDto { TeacherId = Teacher1Id, Name = "First", Surname = "Teacher" };
            _groupsService.AddGroupEntry(newGroupName, selectedCourse, selectedTeacher);

            Assert.Fail("Expected Exception was not thrown.");
        }
        catch (Exception actualError)
        {
            Assert.AreEqual(expectedErrorMessage, actualError.Message);
        }
    }

    [TestMethod]
    public void Test_AddGroupEntry_TeacherNotFoundCase()
    {
        var expectedErrorMessage = "Teacher 'WRONG Teacher' not found!";

        try
        {
            var newGroupName = "TestGrp-06";
            var selectedCourse = new CourseDto { CourseId = Course1Id, CourseName = "Test course", Description = "This is test course" };
            var selectedTeacher = new TeacherDto { TeacherId = Guid.NewGuid(), Name = "WRONG", Surname = "Teacher" };
            _groupsService.AddGroupEntry(newGroupName, selectedCourse, selectedTeacher);

            Assert.Fail("Expected Exception was not thrown.");
        }
        catch (Exception actualError)
        {
            Assert.AreEqual(expectedErrorMessage, actualError.Message);
        }
    }
}
