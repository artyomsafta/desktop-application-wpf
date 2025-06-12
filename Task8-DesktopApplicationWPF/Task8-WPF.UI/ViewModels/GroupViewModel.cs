using System.Collections.ObjectModel;

namespace Task8_WPF.UI.ViewModels;

public class GroupViewModel : BaseViewModel
{
    public string GroupName { get; set; }
    public ObservableCollection<StudentViewModel> Students { get; set; } = new();
}
