using System.Collections.ObjectModel;
using Task8_WPF.BAL.Services;

namespace Task8_WPF.UI.ViewModels;

public class TreeViewModel : BaseViewModel
{
    private TreeViewService _treeViewService;
    public ObservableCollection<CourseViewModel> Courses { get; } = new();

    public TreeViewModel()
    {
        _treeViewService = new TreeViewService();
        LoadHierarchy();
    }

    private void LoadHierarchy()
    {
        Courses.Clear();
        var coursesTreeDtos = _treeViewService.GetHierarchyForTreeView();

        foreach (var coursesTreeDto in coursesTreeDtos)
        {
            var courseViewModel = new CourseViewModel
            {
                CourseName = coursesTreeDto.CourseName,
                Groups = new ObservableCollection<GroupViewModel>(coursesTreeDto.Groups.Select(groupsTreeDto => new GroupViewModel
                {
                    GroupName = groupsTreeDto.GroupName,
                    Students = new ObservableCollection<StudentViewModel>(groupsTreeDto.Students.Select(studentsTreeDto => new StudentViewModel
                    {
                        FullName = studentsTreeDto.FullName
                    }))
                }))
            };
            Courses.Add(courseViewModel);
        }
    }
}
