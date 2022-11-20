using System.Threading.Tasks;
using System.Windows.Input;
using AutoMapper;
using Polimer.Data.Repository.Factory;

namespace Polimer.App.ViewModel.Admin;

public class AdminViewModel : ViewModelBase
{
    private UsersViewModel _usersVm;
    private MaterialsViewModel _materialsVm;
    private AdditiveViewModel _additiveVm;
    private CompatibilityMaterialViewModel _compatibilityVm;

    private AdminViewModel(
        IMapper mapper, 
        RepositoriesFactory repositoriesFactory
        )
    {
        _usersVm = UsersViewModel
            .CreateInstance(repositoriesFactory.CreateUserRepository(), mapper);
        _materialsVm = MaterialsViewModel
            .CreateInstance(repositoriesFactory.CreateMaterialRepository(), mapper);
        _additiveVm = AdditiveViewModel
            .CreateInstance(repositoriesFactory.CreateAdditiveRepository(), mapper);
        _compatibilityVm = CompatibilityMaterialViewModel
            .CreateInstance(repositoriesFactory.CreateCompatibilityMaterialrRepository(),
                mapper,
                repositoriesFactory.CreateMaterialRepository());

        UpdateTablesCommand = new AsyncCommand(UpdateTablesAsync, () => true);
        UpdateTablesAsync();
    }

    internal static AdminViewModel CreateInstance(IMapper mapper,
        RepositoriesFactory repositoriesFactory)
    {
        return new AdminViewModel(mapper, repositoriesFactory);
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

    public CompatibilityMaterialViewModel CompatibilityVM
    {
        get => _compatibilityVm;
        set => SetField(ref _compatibilityVm, value);
    }

    public ICommand UpdateTablesCommand { get; set; }

    private async Task UpdateTablesAsync()
    {
        await UsersVM.UpdateEntitiesAsync();
        await MaterialsVM.UpdateEntitiesAsync();
        await AdditiveVM.UpdateEntitiesAsync();
        await CompatibilityVM.UpdateEntitiesAsync();
    }
}