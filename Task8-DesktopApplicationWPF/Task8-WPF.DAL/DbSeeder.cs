using Task8_WPF.DAL.Entities;

namespace Task8_WPF.DAL;

public class DbSeeder
{
    public void Seed(WpfAppDbContext context)
    {
        if (context.Courses.Any() || context.Teachers.Any() || context.Groups.Any() || context.Students.Any())
        {
            return;
        }

        // Courses table seeding
        var cCharpCourse = new Course { Name = "C#/.NET", Description = "C#/.NET mentoring course" };
        var javaCourse = new Course { Name = "Java", Description = "Java Spring mentoring course" };
        var frontEndCourse = new Course { Name = "Front End", Description = "React or Angular or Vue.js mentoring course" };
        var iOsCourse = new Course { Name = "iOS", Description = "iOS (SWIFT) development mentoring course" };
        var pythonCourse = new Course { Name = "Python", Description = "Python mentoring course (Flask, Django, FastAPI)" };

        // Teachers table seeding
        var cCharpTeacher1 = new Teacher { Name = "Herbert", Surname = "Schildt" };
        var cCharpTeacher2 = new Teacher { Name = "Anders", Surname = "Hejlsberg" };
        var javaTeacher1 = new Teacher { Name = "Serhii", Surname = "Nemchynskyi" };
        var javaTeacher2 = new Teacher { Name = "James", Surname = "Gosling" };
        var frontEndTeacher1 = new Teacher { Name = "Timur", Surname = "Shemsedinov" };
        var iOsTeacher1 = new Teacher { Name = "Stephen", Surname = "Wozniak" };
        var pythonTeacher1 = new Teacher { Name = "Terry", Surname = "Gilliam" };

        // Groups table seeding
        var group1 = new Group { Name = "SR-01", Course = cCharpCourse, Teacher = cCharpTeacher1 };
        var group2 = new Group { Name = "SR-02", Course = cCharpCourse, Teacher = cCharpTeacher2 };
        var group3 = new Group { Name = "SR-03", Course = javaCourse, Teacher = javaTeacher1 };
        var group4 = new Group { Name = "SR-04", Course = javaCourse, Teacher = javaTeacher2 };
        var group5 = new Group { Name = "SR-05", Course = frontEndCourse, Teacher = frontEndTeacher1 };
        var group6 = new Group { Name = "SR-06", Course = iOsCourse, Teacher = iOsTeacher1 };
        var group7 = new Group { Name = "SR-07", Course = pythonCourse, Teacher = pythonTeacher1 };

        // Students table seeding
        var students = new List<Student>
        {
            new Student { Name = "Lybov", Surname = "Pavlova", Group = group1 },
            new Student { Name = "Talia", Surname = "Grimsley", Group = group1 },
            new Student { Name = "Artem", Surname = "Timchenko", Group = group1 },
            new Student { Name = "Jaxon", Surname = "Moorland", Group = group1 },
            new Student { Name = "Tessa", Surname = "Winsley", Group = group1 },
            new Student { Name = "Elara", Surname = "Mendez", Group = group1 },
            new Student { Name = "Kian", Surname = "Halbrook", Group = group1 },
            new Student { Name = "Caleb", Surname = "Raycroft", Group = group1 },
            new Student { Name = "Milo", Surname = "Penrose", Group = group1 },
            new Student { Name = "Aria", Surname = "Lockridge", Group = group1 },
            new Student { Name = "Dorian", Surname = "Salloway", Group = group1 },
            new Student { Name = "Freya", Surname = "Delling", Group = group1 },
            new Student { Name = "Keira", Surname = "Voss", Group = group2 },
            new Student { Name = "Veda", Surname = "Elridge", Group = group2 },
            new Student { Name = "Pavlo", Surname = "Timoshenko", Group = group2 },
            new Student { Name = "Kristina", Surname = "Belyaeva", Group = group2 },
            new Student { Name = "Theron", Surname = "Winscott", Group = group2 },
            new Student { Name = "Ihor", Surname = "Potapov", Group = group2 },
            new Student { Name = "Anya", Surname = "Rowntree", Group = group3 },
            new Student { Name = "Dmytro", Surname = "Arnaut", Group = group3 },
            new Student { Name = "Mira", Surname = "Hawthorne", Group = group3 },
            new Student { Name = "Daxton", Surname = "Ingram", Group = group3 },
            new Student { Name = "Oleksandr", Surname = "Sidorov", Group = group3 },
            new Student { Name = "Callum", Surname = "Thorne", Group = group3 },
            new Student { Name = "Selene", Surname = "Bromley", Group = group3 },
            new Student { Name = "Elior", Surname = "Baskett", Group = group3 },
            new Student { Name = "Lyra", Surname = "Drummond", Group = group3 },
            new Student { Name = "Jareth", Surname = "Whitcombe", Group = group3 },
            new Student { Name = "Eira", Surname = "Copeland", Group = group4 },
            new Student { Name = "Niko", Surname = "Draycott", Group = group4 },
            new Student { Name = "Ryker", Surname = "Tilling", Group = group4 },
            new Student { Name = "Nia", Surname = "Redmont", Group = group4 },
            new Student { Name = "Liora", Surname = "Westfield", Group = group4 },
            new Student { Name = "Semen", Surname = "Petrov", Group = group4 },
            new Student { Name = "Kostiantyn", Surname = "Ponomarenko", Group = group4 },
            new Student { Name = "Zane", Surname = "Weatherly", Group = group4 },
            new Student { Name = "Lucan", Surname = "Astley", Group = group4 },
            new Student { Name = "Denis", Surname = "Fedorenko", Group = group4 },
            new Student { Name = "Nyla", Surname = "Flint", Group = group4 },
            new Student { Name = "Isla", Surname = "Merrick", Group = group4 },
            new Student { Name = "Nolan", Surname = "Farnsworth", Group = group4 },
            new Student { Name = "Vitaliy", Surname = "Spiridonov", Group = group4 },
            new Student { Name = "Aziel", Surname = "Brandell", Group = group5 },
            new Student { Name = "Aiden", Surname = "Corvell", Group = group5 },
            new Student { Name = "Maeve", Surname = "Chilton", Group = group5 },
            new Student { Name = "Anastasiya", Surname = "Alekseeva", Group = group5 },
            new Student { Name = "Zev", Surname = "Blackwood", Group = group5 },
            new Student { Name = "Tamara", Surname = "Sidorova", Group = group5 },
            new Student { Name = "Cassian", Surname = "Trask", Group = group5 },
            new Student { Name = "Raya", Surname = "Thornwell", Group = group5 },
            new Student { Name = "Yulia", Surname = "Shevchuk", Group = group5 },
            new Student { Name = "Maksym", Surname = "Zaporojets", Group = group6 },
            new Student { Name = "Stellan", Surname = "Ainsworth", Group = group6 },
            new Student { Name = "Junia", Surname = "Coldwell", Group = group6 },
            new Student { Name = "Olena", Surname = "Abramova", Group = group6 },
            new Student { Name = "Alina", Surname = "Fortner", Group = group6 },
            new Student { Name = "Orion", Surname = "Rathbone", Group = group6 },
            new Student { Name = "Andriy", Surname = "Potapov", Group = group6 },
            new Student { Name = "Ronan", Surname = "Winslow", Group = group6 },
            new Student { Name = "Finn", Surname = "Averill", Group = group6 },
            new Student { Name = "Kaela", Surname = "Vexley", Group = group6 },
            new Student { Name = "Mychaylo", Surname = "Smirnov", Group = group6 },
            new Student { Name = "Ganna", Surname = "Golub", Group = group6 },
            new Student { Name = "Elina", Surname = "Dorrance", Group = group6 },
            new Student { Name = "Bohdan", Surname = "Ivanov", Group = group6 },
            new Student { Name = "Emrys", Surname = "Kessler", Group = group6 },
            new Student { Name = "Sariah", Surname = "Fenwick", Group = group6 },
            new Student { Name = "Oleg", Surname = "Marchenko", Group = group6 },
            new Student { Name = "Ivan", Surname = "Soroka", Group = group7 },
            new Student { Name = "Lennox", Surname = "Arkwright", Group = group7 },
            new Student { Name = "Olga", Surname = "Petrenko", Group = group7 },
            new Student { Name = "Liana", Surname = "Corwin", Group = group7 },
            new Student { Name = "Serhii", Surname = "Sidorov", Group = group7 },
            new Student { Name = "Mark", Surname = "Ivanov", Group = group7 },
            new Student { Name = "Amira", Surname = "Tolland", Group = group7 },
            new Student { Name = "Kieran", Surname = "Wendell", Group = group7 }
        };

        context.Courses.AddRange(cCharpCourse, javaCourse, frontEndCourse, iOsCourse, pythonCourse);
        context.Teachers.AddRange(cCharpTeacher1, cCharpTeacher2, javaTeacher1, javaTeacher2, frontEndTeacher1, iOsTeacher1, pythonTeacher1);
        context.Groups.AddRange(group1, group2, group3, group4, group5, group6, group7);
        context.Students.AddRange(students);

        context.SaveChanges();
    }
}
