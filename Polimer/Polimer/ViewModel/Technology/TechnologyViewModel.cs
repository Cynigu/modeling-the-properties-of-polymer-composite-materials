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
    private readonly IFileService _fileService;
    private readonly IDialogService _dialogService;
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
    private double _percentResearchFirst;
    private double _percentResearchSecond;
    private double _percentResearchAdditive;
    private bool _isComputed;
    private ObservableCollection<CompatibilityMaterialModel> _compatibilityMaterialsForSecondMaterial;
    private CompatibilityMaterialModel? _selectedSCompatibilityMaterialSecond;
    private bool _isResearch;
    private ComputeRecipeParametersModel? _computeRecipeParametersResearchModel;

    public TechnologyViewModel(IMapper mapper, RepositoriesFactory repositories, IFileService fileService, IDialogService dialogService)
    {
        _compatibilityMaterialRepository = repositories.CreateCompatibilityMaterialrRepository();
        _recipeRepository = repositories.CreateRecipeRepository();
        _mapper = mapper;
        this._fileService = fileService;
        this._dialogService = dialogService;
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
        PercentResearchFirst = 100;
        PercentResearchSecond = 10;
        PercentResearchAdditive = 10;
        IsComputed = false;
        IsResearch = false;

        ComputeRecipeParametersModel = new ComputeRecipeParametersModel();

        ComputeCommand = new AsyncCommand(() => ComputeParametersAsync(false), CanCompute);

        ResearchCommand = new AsyncCommand(() => ComputeParametersAsync(true), CanCompute);

        SaveReportCommand = new AsyncCommand(SaveReport, () => IsComputed);
    }

    public bool CanCompute()
    {
        var b = SelectedAdditive != null && SelectedCompatibilityMaterial != null
                                        && SelectedUsefulProductModel != null
                                        && PropertyAdditive != null
                                        && PropertyFirst != null
                                        && PropertySecond != null
                                        && SelectedSCompatibilityMaterialSecond != null;
        if (!b)
        {
            IsComputed = false;
            IsResearch = false;
            ComputeRecipeParametersResearchModel = null;
            ComputeRecipeParametersModel = null;
        }

        return b;
    }

    public bool IsComputed
    {
        get
        {
            return _isComputed;
        }
        set => SetField(ref _isComputed, value);
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
        set
        {
            SetField(ref _compatibilityMaterials, value);
        }
    }

    public ObservableCollection<CompatibilityMaterialModel> CompatibilityMaterialsForSecondMaterial
    {
        get => _compatibilityMaterialsForSecondMaterial;
        set => SetField(ref _compatibilityMaterialsForSecondMaterial, value);
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
                UsefulProducts = _mapper.Map<ObservableCollection<UsefulProductModel>>(_usefulProductRepository
                    .GetEntitiesByFilterAsync(x =>
                        x.Recipe.CompatibilityMaterial.Id == _compatibilityMaterial.Id).Result);
                
                CompatibilityMaterialsForSecondMaterial = _mapper
                    .Map<ObservableCollection<CompatibilityMaterialModel>>(
                        _compatibilityMaterialRepository.GetEntitiesByFilterAsync(x =>
                            x.Id == _compatibilityMaterial.Id).Result);
            }
        }
    }

    public CompatibilityMaterialModel? SelectedSCompatibilityMaterialSecond
    {
        get => _selectedSCompatibilityMaterialSecond;
        set => SetField(ref _selectedSCompatibilityMaterialSecond, value);
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

    public double PercentResearchFirst
    {
        get => _percentResearchFirst;
        set => SetField(ref _percentResearchFirst, value);
    }

    public double PercentResearchSecond
    {
        get => _percentResearchSecond;
        set => SetField(ref _percentResearchSecond, value);
    }

    public double PercentResearchAdditive
    {
        get => _percentResearchAdditive;
        set => SetField(ref _percentResearchAdditive, value);
    }

    #endregion

    public ComputeRecipeParametersModel? ComputeRecipeParametersModel
    {
        get => _computeRecipeParametersModel;
        set => SetField(ref _computeRecipeParametersModel, value);
    }

    public ComputeRecipeParametersModel? ComputeRecipeParametersResearchModel
    {
        get => _computeRecipeParametersResearchModel;
        set => SetField(ref _computeRecipeParametersResearchModel, value);
    }

    public bool IsResearch
    {
        get => _isResearch;
        set => SetField(ref _isResearch, value);
    }

    #endregion

    #region Commands

    public ICommand ComputeCommand { get; set; }
    public ICommand ResearchCommand { get; set; }
    public ICommand SaveReportCommand { get; set; }


    #endregion

    #region Methods

    private async Task SaveReport()
    {
        try
        {
            if (_dialogService.SaveFileDialog())
            {
                if (ComputeRecipeParametersModel != null)
                    _fileService.Save(_dialogService.FilePath, ComputeRecipeParametersModel, ComputeRecipeParametersResearchModel);
                else
                {
                    throw new InvalidOperationException("Нечего сохранять!");
                }
                _dialogService.ShowMessage("Файл сохранен");
            }
        }
        catch (Exception ex)
        {
            _dialogService.ShowMessage(ex.Message);
        }
    }
    private async Task ComputeParametersAsync(bool useResearch)
    {
        if (useResearch)
        {
            await ComputeParametersResearchAsync();
        }
        else
        {
            await ComputeParametersAsync();
        }
    }
    private async Task ComputeParametersResearchAsync()
    {

        ComputeRecipeParametersResearchModel = new ComputeRecipeParametersModel();

        ComputeRecipeParametersResearchModel.UsefulProduct = _mapper.Map<UsefulProductModel>(SelectedUsefulProductModel);

        // Получение рецептуры смеси
        ComputeRecipeParametersResearchModel.Recipe = _mapper.Map<RecipeModel>(_recipeRepository
            .GetEntityByFilterFirstOrDefaultAsync(r =>
                r.IdAdditive == SelectedAdditive!.Id && r.IdCompatibilityMaterial == SelectedCompatibilityMaterial!.Id).Result);
        ComputeRecipeParametersResearchModel.Recipe.ContentMaterialFirst = PercentResearchFirst;
        ComputeRecipeParametersResearchModel.Recipe.ContentMaterialSecond = PercentResearchSecond;
        ComputeRecipeParametersResearchModel.Recipe.ContentAdditive = PercentResearchAdditive;


        // Получение объема смеси
        ComputeRecipeParametersResearchModel.TotalVolume = PropertyUsefulProducts!
            .FirstOrDefault(x => x.Property.Name == "Объём")!.Value;

        var percents = new[]
        {
            ComputeRecipeParametersResearchModel.Recipe.ContentMaterialFirst,
            ComputeRecipeParametersResearchModel.Recipe.ContentMaterialSecond,
            ComputeRecipeParametersResearchModel.Recipe.ContentAdditive
        };
        var densities = new[] { PropertyFirst!.Density, PropertySecond!.Density, PropertyAdditive!.Density };

        // Получение плотности смеси
        ComputeRecipeParametersResearchModel.Density = Math.Round(CalculatePhysicsService.GetDensity(percents, ComputeRecipeParametersResearchModel.TotalVolume, densities), 2);

        var viscosities = new[]
        {
            PropertyFirst.Viscosity, PropertySecond.Viscosity, PropertyAdditive.Viscosity
        };

        // Получение вязкости смеси
        ComputeRecipeParametersResearchModel.Viscosity = Math.Round(CalculatePhysicsService.GetViscosity(percents, viscosities,
            densities, ComputeRecipeParametersResearchModel.TotalVolume), 2);

        // Получение количества фаз смеси
        ComputeRecipeParametersResearchModel.NumberOfPhases = CalculatePhysicsService.GetNumberOfPhases();

        // Получение ПТР 
        var t = PropertyUsefulProducts!.FirstOrDefault(x => x.Property.Name == "Время")!.Value;
        var k = PropertyUsefulProducts!.FirstOrDefault(x => x.Property.Name == "Количество зон")!.Value;

        double? ptr = PropertyFirst.QuickName == "ПЭНД" ? null : 2.59;
        ComputeRecipeParametersResearchModel.Ptr = Math.Round(CalculatePhysicsService.GetPtr(t, k, ComputeRecipeParametersResearchModel.TotalVolume, percents, densities), 2);

        // Получение растворимости
        var constMolec = new[]
        {
            PropertyFirst.ConstMol, PropertySecond.ConstMol, PropertyAdditive.ConstMol
        };

        double[] qRast = new[]
        {
            PropertyFirst.KRast, PropertySecond.KRast, PropertyAdditive.KRast
        };

        ComputeRecipeParametersResearchModel.Solubility = Math.Round(CalculatePhysicsService.GetSolubility(qRast, percents, ComputeRecipeParametersResearchModel.TotalVolume), 2);

        // Получение насыпной плотности
        var NMass = new[] { PropertyFirst!.NMass, PropertySecond!.NMass, PropertyAdditive!.NMass };
        ComputeRecipeParametersResearchModel.NasDensity = Math.Round(CalculatePhysicsService.GetNDensity(NMass, ComputeRecipeParametersResearchModel.TotalVolume, percents), 2);

        MessageBox.Show("Расчёт выполнен!");

        IsComputed = true;
        IsResearch = true;

    }

    private async Task ComputeParametersAsync()
    {

        ComputeRecipeParametersModel = new ComputeRecipeParametersModel();

        ComputeRecipeParametersModel.UsefulProduct = _mapper.Map<UsefulProductModel>(SelectedUsefulProductModel);

        // Получение рецептуры смеси
        ComputeRecipeParametersModel.Recipe = _mapper.Map<RecipeModel>(_recipeRepository
            .GetEntityByFilterFirstOrDefaultAsync(r =>
                r.IdAdditive == SelectedAdditive!.Id && r.IdCompatibilityMaterial == SelectedCompatibilityMaterial!.Id).Result);

        // Получение объема смеси
        ComputeRecipeParametersModel.TotalVolume = PropertyUsefulProducts!
            .FirstOrDefault(x => x.Property.Name == "Объём")!.Value;

        var percents = new[]
        {
            ComputeRecipeParametersModel.Recipe.ContentMaterialFirst,
            ComputeRecipeParametersModel.Recipe.ContentMaterialSecond,
            ComputeRecipeParametersModel.Recipe.ContentAdditive
        };
        var densities = new[] {PropertyFirst!.Density, PropertySecond!.Density, PropertyAdditive!.Density};

        // Получение плотности смеси
        ComputeRecipeParametersModel.Density = Math.Round( CalculatePhysicsService.GetDensity( percents, ComputeRecipeParametersModel.TotalVolume, densities), 2);

        var viscosities = new[]
        {
            PropertyFirst.Viscosity, PropertySecond.Viscosity, PropertyAdditive.Viscosity
        };

        // Получение вязкости смеси
        ComputeRecipeParametersModel.Viscosity = Math.Round( CalculatePhysicsService.GetViscosity(percents, viscosities,
            densities, ComputeRecipeParametersModel.TotalVolume), 2);

        // Получение количества фаз смеси
        ComputeRecipeParametersModel.NumberOfPhases = CalculatePhysicsService.GetNumberOfPhases();

        // Получение ПТР 
        var t = PropertyUsefulProducts!.FirstOrDefault(x => x.Property.Name == "Время")!.Value;
        var k = PropertyUsefulProducts!.FirstOrDefault(x => x.Property.Name == "Количество зон")!.Value;

        double? ptr = PropertyFirst.QuickName == "ПЭНД" ? null : 2.59;
        ComputeRecipeParametersModel.Ptr = Math.Round( CalculatePhysicsService.GetPtr(t, k, ComputeRecipeParametersModel.TotalVolume, percents, densities), 2);

        // Получение растворимости
        var constMolec = new[]
        {
            PropertyFirst.ConstMol, PropertySecond.ConstMol, PropertyAdditive.ConstMol
        };

        double[] qRast = new[]
        {
            PropertyFirst.KRast, PropertySecond.KRast, PropertyAdditive.KRast
        };

        ComputeRecipeParametersModel.Solubility = Math.Round(CalculatePhysicsService.GetSolubility( qRast, percents, ComputeRecipeParametersModel.TotalVolume), 2);

        // Получение насыпной плотности
        var NMass = new[] { PropertyFirst!.NMass, PropertySecond!.NMass, PropertyAdditive!.NMass };
        ComputeRecipeParametersModel.NasDensity = Math.Round(CalculatePhysicsService.GetNDensity(NMass, ComputeRecipeParametersModel.TotalVolume, percents), 2);

        MessageBox.Show("Расчёт выполнен!");

        IsComputed = true;
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
        PropertyFirst.ConstMol = PropertiesFirstMaterial!.FirstOrDefault(x => x.Property.Name == "Мольная константа")!.Value;
        PropertyFirst.NMass = PropertiesFirstMaterial!.FirstOrDefault(x => x.Property.Name == "Насыпная масса")!.Value;
        PropertyFirst.KRast = PropertiesFirstMaterial!.FirstOrDefault(x => x.Property.Name == "Параметр растворимости")!.Value;

        PropertySecond = new PropertyElement();
        PropertySecond.QuickName = compatibilityMaterial.SecondMaterial.QuickName;
        PropertySecond.EnglishName = compatibilityMaterial.SecondMaterial.EnglishName;
        PropertySecond.RussianName = compatibilityMaterial.SecondMaterial.RussianName;
        PropertySecond.Density = PropertiesSecondMaterial!.FirstOrDefault(x => x.Property.Name == "Плотность при 20 С")!.Value;
        PropertySecond.Viscosity = PropertiesSecondMaterial!.FirstOrDefault(x => x.Property.Name == "Вязкость")!.Value;
        PropertySecond.MolecMass = PropertiesSecondMaterial!.FirstOrDefault(x => x.Property.Name == "Средняя молекулярная масса")!.Value;
        PropertySecond.ConstMol = PropertiesSecondMaterial!.FirstOrDefault(x => x.Property.Name == "Мольная константа")!.Value;
        PropertySecond.NMass = PropertiesSecondMaterial!.FirstOrDefault(x => x.Property.Name == "Насыпная масса")!.Value;
        PropertySecond.KRast = PropertiesSecondMaterial!.FirstOrDefault(x => x.Property.Name == "Параметр растворимости")!.Value;

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
        PropertyAdditive.ConstMol = PropertiesAdditive!.FirstOrDefault(x => x.Property.Name == "Мольная константа")!.Value;
        PropertyAdditive.NMass = PropertiesAdditive!.FirstOrDefault(x => x.Property.Name == "Насыпная масса")!.Value;
        PropertyAdditive.KRast = PropertiesAdditive!.FirstOrDefault(x => x.Property.Name == "Параметр растворимости")!.Value;

    }
    #endregion
}

