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
    private UsefulProductViewModel _usefulProductVm;
    private UnitViewModel _unitsVm;
    private PropertiesViewModel _propertiesVm;
    private PropertyMaterialViewModel _propertyMaterialVm;
    private PropertyUsefulProductViewModel _propertyUsefulProductVm;
    private RecipeViewModel _recipesVm;
    private PropertyAdditiveViewModel _propertyAdditiveVm;

    private AdminViewModel(
        IMapper mapper, 
        RepositoriesFactory repositoriesFactory
        )
    {
        _usefulProductVm = UsefulProductViewModel
            .CreateInstance(repositoriesFactory.CreateUsefulProductRepository(), mapper, repositoriesFactory.CreateRecipeRepository());
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
        _unitsVm = UnitViewModel.CreateInstance(repositoriesFactory.CreateUnitRepository(), mapper);
        _propertiesVm = PropertiesViewModel
            .CreateInstance(
                repositoriesFactory.CreatePropertyRepository(), 
                mapper,
                repositoriesFactory.CreateUnitRepository());
        _propertyMaterialVm = PropertyMaterialViewModel.CreateInstance(
            repositoriesFactory.CreatePropertyMaterialRepository(), mapper,
            repositoriesFactory.CreateMaterialRepository(),
            repositoriesFactory.CreatePropertyRepository());
        
        _propertyUsefulProductVm = PropertyUsefulProductViewModel.CreateInstance(
            repositoriesFactory.CreatePropertyUsefulProductRepository(), mapper,
            repositoriesFactory.CreateUsefulProductRepository(),
            repositoriesFactory.CreatePropertyRepository());

        _recipesVm = RecipeViewModel.CreateInstance(repositoriesFactory.CreateRecipeRepository(), mapper,
            repositoriesFactory.CreateCompatibilityMaterialrRepository(),
            repositoriesFactory.CreateAdditiveRepository());

        _propertyAdditiveVm = PropertyAdditiveViewModel.CreateInstance(repositoriesFactory.CreatePropertyAdditiveRepository(), mapper,
            repositoriesFactory.CreateAdditiveRepository(), repositoriesFactory.CreatePropertyRepository());

        UpdateTablesCommand = new AsyncCommand(UpdateTablesAsync, () => true);
        UpdateTablesAsync();
    }

    internal static AdminViewModel CreateInstance(IMapper mapper,
        RepositoriesFactory repositoriesFactory)
    {
        return new AdminViewModel(mapper, repositoriesFactory);
    }

    public PropertyAdditiveViewModel PropertyAdditiveVM
    {
        get => _propertyAdditiveVm;
        set => SetField(ref _propertyAdditiveVm, value);
    }

    public RecipeViewModel RecipesVM
    {
        get => _recipesVm;
        set => SetField(ref _recipesVm, value);
    }

    public PropertyUsefulProductViewModel PropertyUsefulProductVm
    {
        get => _propertyUsefulProductVm;
        set => SetField(ref _propertyUsefulProductVm, value);
    }

    public PropertyMaterialViewModel PropertyMaterialVM
    {
        get => _propertyMaterialVm;
        set => SetField(ref _propertyMaterialVm, value);
    }

    public UnitViewModel UnitsVM
    {
        get => _unitsVm;
        set => SetField(ref _unitsVm, value);
    }

    public PropertiesViewModel PropertiesVM
    {
        get => _propertiesVm;
        set => SetField(ref _propertiesVm, value);
    }

    public UsefulProductViewModel UsefulProductVm
    {
        get => _usefulProductVm;
        set => SetField(ref _usefulProductVm, value);
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
        await UsefulProductVm.UpdateEntitiesAsync();
        await UnitsVM.UpdateEntitiesAsync();
        await PropertiesVM.UpdateEntitiesAsync();
        await PropertyMaterialVM.UpdateEntitiesAsync();
        await PropertyUsefulProductVm.UpdateEntitiesAsync();
        await RecipesVM.UpdateEntitiesAsync();
        await PropertyAdditiveVM.UpdateEntitiesAsync();
    }
}