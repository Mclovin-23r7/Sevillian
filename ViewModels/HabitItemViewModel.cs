using CommunityToolkit.Mvvm.ComponentModel;
using Sevillian.Model;

namespace Sevillian.ViewModels;

public partial class HabitItemViewModel(HabitItem item) : ViewModelBase
{
    private int _Id = item.Id;
    private string _Name = item.HabitName;
    private string? _Desc = item.HabitDesc;
}