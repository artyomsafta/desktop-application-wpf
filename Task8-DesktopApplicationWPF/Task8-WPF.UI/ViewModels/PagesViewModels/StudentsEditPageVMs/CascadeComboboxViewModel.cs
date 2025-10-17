using System.Collections.ObjectModel;
using Task8_WPF.BAL.Dto.TreeDtos;
using Task8_WPF.BAL.Services;

namespace Task8_WPF.UI.ViewModels.PagesViewModels.StudentsEditPageVMs;

public class CascadeComboboxViewModel : BaseViewModel
{
    private readonly TreeViewService _treeViewService;

    public ObservableCollection<CoursesTreeDto> Courses { get; }
    public ObservableCollection<GroupsTreeDto> Groups { get; } = new();
    public ObservableCollection<StudentsTreeDto> Students { get; } = new();

    private CoursesTreeDto _selectedCourse;
    public CoursesTreeDto SelectedCourse
    {
        get => _selectedCourse;
        set
        {
            if (SetProperty(ref _selectedCourse, value))
            {
                LoadGroups();
                Students.Clear();
            }
        }
    }

    private GroupsTreeDto _selectedGroup;
    public GroupsTreeDto SelectedGroup
    {
        get => _selectedGroup;
        set
        {
            if (SetProperty(ref _selectedGroup, value))
            {
                LoadStudents();
            }
        }
    }

    public StudentsTreeDto SelectedStudent { get; set; }

    public CascadeComboboxViewModel()
    {
        _treeViewService = new TreeViewService();
        Courses = new ObservableCollection<CoursesTreeDto>(_treeViewService.GetHierarchyForTreeView());
    }

    private void LoadGroups()
    {
        Groups.Clear();
        if (SelectedCourse != null)
        {
            foreach (var g in SelectedCourse.Groups)
                Groups.Add(g);
        }
    }

    private void LoadStudents()
    {
        Students.Clear();
        if (SelectedGroup != null)
        {
            foreach (var s in SelectedGroup.Students)
                Students.Add(s);
        }
    }
}
