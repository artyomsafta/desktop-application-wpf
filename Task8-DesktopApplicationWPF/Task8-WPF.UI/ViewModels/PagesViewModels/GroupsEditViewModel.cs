using Task8_WPF.UI.ViewModels.PagesViewModels.GroupsEditPageVMs;

namespace Task8_WPF.UI.ViewModels;

public class GroupsEditViewModel : BaseViewModel
{
    public GroupsListViewModel GroupsListViewModel { get; }

    public GroupsEditViewModel()
    {
        GroupsListViewModel = new GroupsListViewModel();
    }
}
