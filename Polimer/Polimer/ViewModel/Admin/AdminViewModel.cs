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
    private MaterialsViewModel _materialsVm;

    private AdminViewModel(IMapper mapper, UserRepository userRepository, MaterialRepository materialRepository)
    {
        _usersVm = UsersViewModel.CreateInstance(userRepository, mapper);
        _materialsVm = MaterialsViewModel.CreateInstance(materialRepository, mapper);
        UpdateTablesCommand = new AsyncCommand(UpdateTablesAsync, () => true);
        UpdateTablesAsync();
    }

    internal static AdminViewModel CreateInstance(IMapper mapper, UserRepository userRepository, MaterialRepository materialRepository)
    {
        return new AdminViewModel(mapper, userRepository, materialRepository);
    }


    public MaterialsViewModel MaterialsVM
    {
        get => _materialsVm;
        set => SetField(ref _materialsVm, value);
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
        await MaterialsVM.UpdateEntitiesAsync();
    }
}