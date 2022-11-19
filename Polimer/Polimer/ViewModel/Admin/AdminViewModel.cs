using System;
using System.Threading.Tasks;
using System.Windows.Input;
using AutoMapper;
using Polimer.Data.Models;
using Polimer.Data.Repository;

namespace Polimer.App.ViewModel.Admin;

public class AdminViewModel : ViewModelBase
{
    private UsersViewModel _usersVm;

    private AdminViewModel(UserRepository userRepository, IMapper mapper)
    {
        _usersVm = UsersViewModel.CreateInstance(userRepository, mapper);
        UpdateTablesCommand = new AsyncCommand(UpdateTablesAsync, () => true);
        UpdateTablesAsync();
    }

    public static AdminViewModel CreateInstance(UserRepository userRepository, IMapper mapper)
    {
        return new AdminViewModel(userRepository, mapper);
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