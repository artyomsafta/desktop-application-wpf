using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.Windows.Input;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.BAL.Services.GroupsServices;
using Task8_WPF.UI.ViewModels.DtoListsViewModels;

namespace Task8_WPF.UI.ViewModels.PagesViewModels.GroupsEditPageVMs;

public class RenameGroupWindowViewModel : BaseViewModel
{
    private GroupRenameService _groupRenameService;

    private string _groupNewName;
    public string GroupNewName 
    {
        get => _groupNewName;
        set
        {
            _groupNewName = value;
            OnPropertyChanged();
            OkCommand.NotifyCanExecuteChanged();
        }
    }

    private GroupDto _selectedGroup;
    public GroupDto SelectedGroup
    {
        get => _selectedGroup;
        set
        {
            _selectedGroup = value;
            OnPropertyChanged();
            OkCommand.NotifyCanExecuteChanged();
        }
    }

    public IRelayCommand OkCommand { get; }
    public ICommand CancelCommand { get; }
    public GroupsListViewModel GroupsListViewModel { get; }

    public RenameGroupWindowViewModel()
    {
        GroupsListViewModel = new GroupsListViewModel();
        OkCommand = new RelayCommand(ExecuteOk, CanExecuteOk);
        CancelCommand = new RelayCommand<Window>(ExecuteCancel);
    }

    private bool CanExecuteOk()
    {
        return !string.IsNullOrWhiteSpace(GroupNewName)
            && SelectedGroup is not null;
    }

    private void ExecuteOk()
    {
        try
        {
            _groupRenameService = new GroupRenameService(_groupNewName, SelectedGroup);
            _groupRenameService.RenameGroup();
            MessageBox.Show($"Operation successful!\nGroup has been renamed.\nGroup new name: {GroupNewName}.");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void ExecuteCancel(Window window)
    {
        window?.Close();
    }
}
