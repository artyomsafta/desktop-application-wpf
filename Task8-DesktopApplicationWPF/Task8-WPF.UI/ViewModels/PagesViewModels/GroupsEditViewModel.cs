using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using Task8_WPF.UI.ViewModels.PagesViewModels.GroupsEditPageVMs;
using Task8_WPF.UI.Views;

namespace Task8_WPF.UI.ViewModels;

public class GroupsEditViewModel : BaseViewModel
{
    public GroupsListViewModel GroupsListViewModel { get; }
    public ICommand OpenCreateGroupWindowCommand { get; }
    public ICommand OpenRenameGroupWindowCommand {  get; }

    public GroupsEditViewModel()
    {
        GroupsListViewModel = new GroupsListViewModel();
        OpenCreateGroupWindowCommand = new RelayCommand(OpenCreateGroupWindow);
        OpenRenameGroupWindowCommand = new RelayCommand(OpenRenameGroupWindow);
    }

    private void OpenCreateGroupWindow()
    {
        var window = new CreateGroupWindowView
        {
            DataContext = new CreateGroupWindowViewModel()
        };

        window.ShowDialog();
    }

    private void OpenRenameGroupWindow()
    {
        var window = new RenameGroupWindowView
        {
            DataContext = new RenameGroupWindowViewModel()
        };

        window.ShowDialog();
    }
}
