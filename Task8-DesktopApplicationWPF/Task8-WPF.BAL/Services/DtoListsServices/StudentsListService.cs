using Microsoft.EntityFrameworkCore;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.DAL;

namespace Task8_WPF.BAL.Services.DtoListsServices
{
    public class StudentsListService
    {
        private WpfAppDbContext _context;

        public StudentsListService(DbContextOptions<WpfAppDbContext> options)
        {
            _context = new WpfAppDbContext(options);
        }

        public StudentsListService()
        {
            _context = new WpfAppDbContext();
        }

        public List<StudentDto> GetStudentsList(string selectedGroupName)
        {
            var groupId = _context.Groups
           .Where(g => g.Name == selectedGroupName)
           .Select(g => g.Id)
           .FirstOrDefault();

            var students = _context.Students
                .Where(g => g.GroupId == groupId)
                .Include(g => g.Group)
                .ToList();

            var studentsList = new List<StudentDto>();

            foreach (var student in students)
            {
                studentsList.Add(new StudentDto
                {
                    Name = student.Name,
                    Surname = student.Surname,
                    GroupName = student.Group.Name
                });
            }

            return studentsList;
        }
    }
}
