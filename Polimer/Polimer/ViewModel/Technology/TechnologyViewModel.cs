using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using AutoMapper;
using Polimer.App.Services;
using Polimer.App.ViewModel.Admin.Models;
using Polimer.App.ViewModel.Technology.Models;
using Polimer.Data.Repository;
using Polimer.Data.Repository.Factory;

namespace Polimer.App.ViewModel.Technology;

/// <summary>
/// view moodel для окна технолога
/// </summary>
public class TechnologyViewModel : ViewModelBase
{
    private readonly IMapper _mapper;
    private readonly CompatibilityMaterialrRepository _compatibilityMaterialRepository;
    private readonly RecipeRepository _recipeRepository;
    private readonly UsefulProductRepository _usefulProductRepository;
    private readonly PropertyUsefulProductRepository _propertyUsefulProductRepository;
    private readonly PropertyAdditiveRepository _propertyAdditiveRepository;
    private readonly PropertyMaterialRepository _propertyMaterialRepository;

    private ComputeRecipeParametersModel? _computeRecipeParametersModel;
    private ObservableCollection<CompatibilityMaterialModel> _compatibilityMaterials;
    private ObservableCollection<AdditiveModel>? _additives;
    private AdditiveModel? _additive;
    private CompatibilityMaterialModel? _compatibilityMaterial;
    private ObservableCollection<UsefulProductModel> _usefulProducts;
    private UsefulProductModel? _selectedUsefulProductModel;
    private Brush _colorUsefulProduct;
    private ObservableCollection<PropertyUsefulProductModel>? _propertyUsefulProducts;
    private ObservableCollection<PropertyMaterialModel>? _propertiesFirstMaterial;
    private ObservableCollection<PropertyMaterialModel>? _propertiesSecondMaterial;
    private ObservableCollection<PropertyAdditiveModel>? _propertiesAdditive;
    private PropertyElement? _propertyFirst;
    private PropertyElement? _propertySecond;
    private PropertyElement? _propertyAdditive;

    public TechnologyViewModel(IMapper mapper, RepositoriesFactory repositories)
    {
        _compatibilityMaterialRepository = repositories.CreateCompatibilityMaterialrRepository();
        _recipeRepository = repositories.CreateRecipeRepository();
        _mapper = mapper;
        _usefulProductRepository = repositories.CreateUsefulProductRepository();
        _propertyUsefulProductRepository = repositories.CreatePropertyUsefulProductRepository();
        _propertyAdditiveRepository = repositories.CreatePropertyAdditiveRepository();
        _propertyMaterialRepository = repositories.CreatePropertyMaterialRepository();

        UsefulProducts = new ObservableCollection<UsefulProductModel>(
            _mapper.Map<ICollection<UsefulProductModel>>( _usefulProductRepository.GetEntitiesAsync().Result));
        CompatibilityMaterials = new ObservableCollection<CompatibilityMaterialModel>( 
            _mapper.Map<ICollection<CompatibilityMaterialModel>>(_compatibilityMaterialRepository.GetEntitiesAsync().Result)
            );
        PropertyUsefulProducts = null;
        Additives = null;
        PropertiesFirstMaterial = null;
        PropertiesAdditive = null;
        PropertiesSecondMaterial = null;
        PropertySecond = null;
        PropertyAdditive = null;
        PropertyFirst = null;

        SelectedAdditive = null;
        SelectedCompatibilityMaterial = null;
        SelectedUsefulProductModel = null;
        
        ComputeRecipeParametersModel = new ComputeRecipeParametersModel();

        ComputeCommand = new AsyncCommand(ComputeParametersAsync, () => 
            SelectedAdditive != null && SelectedCompatibilityMaterial != null
            && SelectedUsefulProductModel != null
            && PropertyAdditive != null
            && PropertyFirst != null
            && PropertySecond != null);
    }

    #region Properties

    #region Input parameters

    public ObservableCollection<PropertyMaterialModel>? PropertiesFirstMaterial
    {
        get => _propertiesFirstMaterial;
        set => SetField(ref _propertiesFirstMaterial, value);
    }

    public ObservableCollection<PropertyMaterialModel>? PropertiesSecondMaterial
    {
        get => _propertiesSecondMaterial;
        set => SetField(ref _propertiesSecondMaterial, value);
    }

    public ObservableCollection<PropertyAdditiveModel>? PropertiesAdditive
    {
        get => _propertiesAdditive;
        set => SetField(ref _propertiesAdditive, value);
    }

    public Brush ColorUsefulProduct
    {
        get => _colorUsefulProduct;
        set => SetField(ref _colorUsefulProduct, value);
    }

    public ObservableCollection<PropertyUsefulProductModel>? PropertyUsefulProducts
    {
        get => _propertyUsefulProducts;
        set => SetField(ref _propertyUsefulProducts, value);
    }

    public ObservableCollection<UsefulProductModel> UsefulProducts
    {
        get => _usefulProducts;
        set => SetField(ref _usefulProducts, value);
    }

    public ObservableCollection<CompatibilityMaterialModel> CompatibilityMaterials
    {
        get => _compatibilityMaterials;
        set => SetField(ref _compatibilityMaterials, value);
    }

    public ObservableCollection<AdditiveModel>? Additives
    {
        get => _additives;
        set => SetField(ref _additives, value);
    }

    public AdditiveModel? SelectedAdditive
    {
        get => _additive;
        set
        {
            SetField(ref _additive, value);
            if (_additive != null)
            {
                InizializePropertyAdditiveAsync(_additive).Wait();
            }
        }
    }

    public CompatibilityMaterialModel? SelectedCompatibilityMaterial
    {
        get => _compatibilityMaterial;
        set
        {
            SetField(ref _compatibilityMaterial, value);
            SelectedAdditive = null;
            if (_compatibilityMaterial != null)
            {
                InitializeAdditivesByCompatabilitiesAsync(_compatibilityMaterial).Wait();
                InizializePropertyMaterialsAsync(_compatibilityMaterial).Wait();
            }
        }
    }

    public UsefulProductModel? SelectedUsefulProductModel
    {
        get => _selectedUsefulProductModel;
        set
        {
            SetField(ref _selectedUsefulProductModel, value);
            if (_selectedUsefulProductModel != null)
                InizializePropertiesUsefulProductAsync(_selectedUsefulProductModel).Wait();

            if (_selectedUsefulProductModel != null && _selectedUsefulProductModel.Name == "Ёмкость")
                ColorUsefulProduct = Brushes.IndianRed;
            else if(_selectedUsefulProductModel != null && _selectedUsefulProductModel.Name == "Поднос")
                ColorUsefulProduct = Brushes.Red;
        }
    }

    public PropertyElement? PropertyFirst
    {
        get => _propertyFirst;
        set => SetField(ref _propertyFirst, value);
    }

    public PropertyElement? PropertySecond
    {
        get => _propertySecond;
        set => SetField(ref _propertySecond, value);
    }

    public PropertyElement? PropertyAdditive
    {
        get => _propertyAdditive;
        set => SetField(ref _propertyAdditive, value);
    }

    #endregion

    public ComputeRecipeParametersModel? ComputeRecipeParametersModel
    {
        get => _computeRecipeParametersModel;
        set => SetField(ref _computeRecipeParametersModel, value);
    }

    #endregion

    #region Commands

    public ICommand ComputeCommand { get; set; }

    #endregion


    #region Methods

    private async Task ComputeParametersAsync()
    {
        ComputeRecipeParametersModel = new ComputeRecipeParametersModel();
        ComputeRecipeParametersModel.Recipe = _mapper.Map<RecipeModel>(_recipeRepository
            .GetEntityByFilterFirstOrDefaultAsync(r =>
                r.IdAdditive == SelectedAdditive!.Id && r.IdCompatibilityMaterial == SelectedCompatibilityMaterial!.Id).Result);

        double percentFirst = ComputeRecipeParametersModel.Recipe.ContentMaterialFirst;
        double percentSecond = ComputeRecipeParametersModel.Recipe.ContentMaterialSecond;
        double percentAdditive = ComputeRecipeParametersModel.Recipe.ContentAdditive;
        double totalPercent = percentAdditive + percentSecond + percentFirst;
        double totalVolume = PropertyUsefulProducts!.FirstOrDefault(x => x.Property.Name == "Объём")!.Value;
      

        ComputeRecipeParametersModel.Density = GetDensity(percentFirst, percentSecond, percentAdditive, totalPercent, totalVolume,
            PropertyFirst!.Density, PropertySecond!.Density, PropertyAdditive!.Density);

        ComputeRecipeParametersModel.Viscosity = CalculatePhysicsService.GetViscosity(PropertyFirst.MolecMass, PropertySecond.MolecMass, 
            PropertyAdditive.MolecMass, PropertyFirst.Viscosity, PropertySecond.Viscosity, PropertyAdditive.Viscosity);

        ComputeRecipeParametersModel.NumberOfPhases = CalculatePhysicsService.GetNumberOfPhases();

        MessageBox.Show("Расчёт выполнен!");
    }

    private double GetDensity(double percentFirst, double percentSecond, double percentAdditive, 
        double totalPercent, double totalVolume, double densityFirst,
        double densitySecond, double densityAdditive)
    {
        // 1. Рассчитать объем для кажого элемента из значений рецептуры и объема полезного изделия 
        var volumeFirst = CalculatePhysicsService.GetVolumeByPercent(percentFirst, totalVolume, totalPercent);
        var volumeSecond = CalculatePhysicsService.GetVolumeByPercent(percentSecond, totalVolume, totalPercent);
        var volumeAdditive = CalculatePhysicsService.GetVolumeByPercent(percentAdditive, totalVolume, totalPercent);

        // 2. Рассчитать массу для каждого элемента по объему для каждого элемента и плотности каждого элемента
        var massFirst = CalculatePhysicsService.GetMass(densityFirst, volumeFirst);
        var massSecond = CalculatePhysicsService.GetMass(densitySecond, volumeSecond);
        var massAdditive = CalculatePhysicsService.GetMass(densityAdditive, volumeAdditive);

        // 3. Ссумировать все массы
        var totalMass = massFirst + massSecond + massAdditive;

        // 4. Найти плонтность смеси
        return Math.Round(CalculatePhysicsService.GetDensity(totalMass, totalVolume), 3);
    }

    private async Task InitializeAdditivesByCompatabilitiesAsync(CompatibilityMaterialModel compatibilityMaterial)
    {
        var additives = (await _recipeRepository
                .GetEntitiesByFilterAsync(a => a.IdCompatibilityMaterial == compatibilityMaterial.Id))
            .Select(a => a.Additive);
        Additives = new ObservableCollection<AdditiveModel>(_mapper.Map<ICollection<AdditiveModel>>(additives));
    }

    private async Task InizializePropertiesUsefulProductAsync(UsefulProductModel usfulProductModel)
    {
        var properties = (await _propertyUsefulProductRepository
                .GetEntitiesByFilterAsync(a => a.IdUsefulProduct == usfulProductModel.Id));

        PropertyUsefulProducts = new ObservableCollection<PropertyUsefulProductModel>(_mapper.Map<ICollection<PropertyUsefulProductModel>>(properties));
    }

    private async Task InizializePropertyMaterialsAsync(CompatibilityMaterialModel compatibilityMaterial)
    {
        var fp = await _propertyMaterialRepository.GetEntitiesByFilterAsync(a =>
            a.IdMaterial == compatibilityMaterial.FirstMaterial.Id);
        PropertiesFirstMaterial = new ObservableCollection<PropertyMaterialModel>(_mapper.Map<ICollection<PropertyMaterialModel>>(fp));

        var sp = await _propertyMaterialRepository.GetEntitiesByFilterAsync(a =>
            a.IdMaterial == compatibilityMaterial.SecondMaterial.Id);
        PropertiesSecondMaterial = new ObservableCollection<PropertyMaterialModel>(_mapper.Map<ICollection<PropertyMaterialModel>>(sp));

        PropertyFirst = new PropertyElement();
        PropertyFirst.QuickName = compatibilityMaterial.FirstMaterial.QuickName;
        PropertyFirst.EnglishName = compatibilityMaterial.FirstMaterial.EnglishName;
        PropertyFirst.RussianName = compatibilityMaterial.FirstMaterial.RussianName;;
        PropertyFirst.Density = PropertiesFirstMaterial!.FirstOrDefault(x => x.Property.Name == "Плотность при 20 С")!.Value;
        PropertyFirst.Viscosity = PropertiesFirstMaterial!.FirstOrDefault(x => x.Property.Name == "Вязкость")!.Value;
        PropertyFirst.MolecMass = PropertiesFirstMaterial!.FirstOrDefault(x => x.Property.Name == "Средняя молекулярная масса")!.Value;

        PropertySecond = new PropertyElement();
        PropertySecond.QuickName = compatibilityMaterial.SecondMaterial.QuickName;
        PropertySecond.EnglishName = compatibilityMaterial.SecondMaterial.EnglishName;
        PropertySecond.RussianName = compatibilityMaterial.SecondMaterial.RussianName;
        PropertySecond.Density = PropertiesSecondMaterial!.FirstOrDefault(x => x.Property.Name == "Плотность при 20 С")!.Value;
        PropertySecond.Viscosity = PropertiesSecondMaterial!.FirstOrDefault(x => x.Property.Name == "Вязкость")!.Value;
        PropertySecond.MolecMass = PropertiesSecondMaterial!.FirstOrDefault(x => x.Property.Name == "Средняя молекулярная масса")!.Value;

    }

    private async Task InizializePropertyAdditiveAsync(AdditiveModel additiveModel)
    {
        var additive = await _propertyAdditiveRepository.GetEntitiesByFilterAsync(a =>
            a.IdAdditive == additiveModel.Id);

        PropertiesAdditive = new ObservableCollection<PropertyAdditiveModel>(_mapper.Map<ICollection<PropertyAdditiveModel>>(additive));

        PropertyAdditive = new PropertyElement();
        PropertyAdditive.QuickName = additiveModel.QuickName;
        PropertyAdditive.RussianName = additiveModel.RussianName;
        PropertyAdditive.EnglishName = additiveModel.EnglishName;
        PropertyAdditive.Density = PropertiesAdditive!.FirstOrDefault(x => x.Property.Name == "Плотность при 20 С")!.Value;
        PropertyAdditive.Viscosity = PropertiesAdditive!.FirstOrDefault(x => x.Property.Name == "Вязкость")!.Value;
        PropertyAdditive.MolecMass = PropertiesAdditive!.FirstOrDefault(x => x.Property.Name == "Средняя молекулярная масса")!.Value;

    }
    #endregion
}

