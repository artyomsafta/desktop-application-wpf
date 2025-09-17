using System.Collections.ObjectModel;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.BAL.Services.DtoListsServices;

namespace Task8_WPF.UI.ViewModels.PagesViewModels.GroupsEditPageVMs;

public class GroupsListViewModel : BaseViewModel
{
    private GroupsListService _groupsService;

    public ObservableCollection<GroupDto> Groups { get; } = new();

    public GroupsListViewModel()
    {
        _groupsService = new GroupsListService();
        LoadList();
    }

    private void LoadList()
    {
        Groups.Clear();

        var groupsDtos = _groupsService.GetGroupsList();

        var sortedGroupsDtos = groupsDtos
            .OrderBy(dto => dto.GroupName)
            .ToList();

        foreach (var sortedGroupsDto in sortedGroupsDtos)
        {
            var groupForListViewModel = new GroupDto
            {
                GroupName = sortedGroupsDto.GroupName,
                CourseName = sortedGroupsDto.CourseName,
                TeacherFullName = sortedGroupsDto.TeacherFullName
            };

            Groups.Add(groupForListViewModel);
        }
    }
}
