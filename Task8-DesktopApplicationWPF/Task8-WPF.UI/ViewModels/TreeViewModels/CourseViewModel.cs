using System.Collections.ObjectModel;

namespace Task8_WPF.UI.ViewModels;

public class CourseViewModel : BaseViewModel
{
    public string CourseName { get; set; }
    public ObservableCollection<GroupViewModel> Groups { get; set; } = new();
}
