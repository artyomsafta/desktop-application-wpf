using System.Collections.ObjectModel;
using Task8_WPF.BAL.Dto;

namespace Task8_WPF.UI.ViewModels;

public class CoursesTreeViewModel : BaseViewModel
{
    public ObservableCollection<CoursesTreeDto> TreeItems { get; set; }

    public CoursesTreeViewModel()
    {
        TreeItems = new ObservableCollection<CoursesTreeDto>
        {
            new CoursesTreeDto
            { 
                Title = "Root", 
                Children = new List<CoursesTreeDto>
                {
                    new CoursesTreeDto { Title = "Child1"},
                    new CoursesTreeDto { Title = "Child2"}
                }
            }
        };
    }
}
