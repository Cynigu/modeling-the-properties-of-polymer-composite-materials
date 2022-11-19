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
    private AdditiveViewModel _additiveVm;

    private AdminViewModel(
        IMapper mapper, 
        UserRepository userRepository, 
        MaterialRepository materialRepository, 
        AdditiveRepository additiveRepository
        )
    {
        _usersVm = UsersViewModel.CreateInstance(userRepository, mapper);
        _materialsVm = MaterialsViewModel.CreateInstance(materialRepository, mapper);
        _additiveVm = AdditiveViewModel.CreateInstance(additiveRepository, mapper);
        UpdateTablesCommand = new AsyncCommand(UpdateTablesAsync, () => true);
        UpdateTablesAsync();
    }

    internal static AdminViewModel CreateInstance(IMapper mapper, UserRepository userRepository, MaterialRepository materialRepository, AdditiveRepository additiveRepository)
    {
        return new AdminViewModel(mapper, userRepository, materialRepository, additiveRepository);
    }

    public MaterialsViewModel MaterialsVM
    {
        get => _materialsVm;
        set => SetField(ref _materialsVm, value);
    }

    public AdditiveViewModel AdditiveVM
    {
        get => _additiveVm;
        set => SetField(ref _additiveVm, value);
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
        await AdditiveVM.UpdateEntitiesAsync();
    }
}