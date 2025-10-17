using System.Collections.ObjectModel;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.BAL.Services;

namespace Task8_WPF.UI.ViewModels.DtoListsViewModels;

public class GroupsListViewModel : BaseViewModel
{
    private DtoListsService _dtoListsService;
    public ObservableCollection<GroupDto> Groups { get; } = new();

    public GroupsListViewModel()
    {
        _dtoListsService = new DtoListsService();
        LoadList();
    }

    private void LoadList()
    {
        Groups.Clear();
        var groupsDtos = _dtoListsService.GetGroupsList();
        var sortedGroupsDtos = groupsDtos
            .OrderBy(dto => dto.GroupName)
            .ToList();

        foreach (var sortedGroupsDto in sortedGroupsDtos)
        {
            var groupForListViewModel = new GroupDto
            {
                GroupId = sortedGroupsDto.GroupId,
                GroupName = sortedGroupsDto.GroupName,
                CourseName = sortedGroupsDto.CourseName,
                TeacherFullName = sortedGroupsDto.TeacherFullName
            };

            Groups.Add(groupForListViewModel);
        }
    }
}
