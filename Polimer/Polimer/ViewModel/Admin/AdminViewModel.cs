using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Polimer.Data.Repository;

namespace Polimer.App.ViewModel.Admin;

public class AdminViewModel : ViewModelBase
{
    private UsersViewModel _usersVm;

    public AdminViewModel(Repository repository)
    {
        _usersVm = new UsersViewModel(repository);
        UpdateTablesCommand = new AsyncCommand(UpdateTablesAsync, () => true);
        UpdateTablesAsync();
    }

    public UsersViewModel UsersVM
    {
        get => _usersVm;
        set => SetField(ref _usersVm, value);
    }

    public ICommand UpdateTablesCommand { get; set; }

    private async Task UpdateTablesAsync()
    {
        await UsersVM.UpdateEntitiesAsync();
    }
}