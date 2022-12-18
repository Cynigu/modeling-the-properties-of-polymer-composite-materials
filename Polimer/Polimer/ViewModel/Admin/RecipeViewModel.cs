using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Polimer.App.ViewModel.Admin.Abstract;
using Polimer.App.ViewModel.Admin.Models;
using Polimer.Data.Models;
using Polimer.Data.Repository;

namespace Polimer.App.ViewModel.Admin;

public class RecipeViewModel
    : TabAdminBaseViewModel<RecipeEntity, RecipeModel>
{
    private readonly CompatibilityMaterialrRepository _materialRepository;
    private readonly AdditiveRepository _additiveRepository;
    private ObservableCollection<CompatibilityMaterialModel> _compatibilityMaterials;
    private ObservableCollection<AdditiveModel> _additives;

    private RecipeViewModel(RecipeRepository repository,
        IMapper mapper,
        CompatibilityMaterialrRepository materialRepository,
        AdditiveRepository additiveRepository) :
        base(repository, mapper)
    {
        _materialRepository = materialRepository;
        _additiveRepository = additiveRepository;
        NameTab = "Рецептура";
        ChangingModel = new RecipeModel();
        var materials = materialRepository.GetEntitiesAsync().Result;
        CompatibilityMaterials = new ObservableCollection<CompatibilityMaterialModel>(mapper.Map<ICollection<CompatibilityMaterialModel>>(materials));
        Additives = new ObservableCollection<AdditiveModel>(mapper.Map<ICollection<AdditiveModel>>(additiveRepository.GetEntitiesAsync().Result));
    }

    public static RecipeViewModel CreateInstance(RecipeRepository repository, IMapper mapper,
        CompatibilityMaterialrRepository materialRepository, AdditiveRepository propertyRepository)
    {
        return new RecipeViewModel(repository, mapper, materialRepository, propertyRepository);
    }

    public ObservableCollection<CompatibilityMaterialModel> CompatibilityMaterials
    {
        get => _compatibilityMaterials;
        set => SetField(ref _compatibilityMaterials, value);
    }

    public ObservableCollection<AdditiveModel> Additives
    {
        get => _additives;
        set => SetField(ref _additives, value);
    }

    /// <summary>
    /// Копирование модели во временную модель
    /// </summary>
    protected override void CopySelectedModelToChanging()
    {
        //ChangingModel.Id = 0;
        ChangingModel.CompatibilityMaterial = (CompatibilityMaterials
            .FirstOrDefault(m => m.Id == SelectedModel?.CompatibilityMaterial.Id))!;
        ChangingModel.Additive = (Additives
            .FirstOrDefault(m => m.Id == SelectedModel?.Additive.Id))!;
        ChangingModel.ContentMaterialFirst = SelectedModel?.ContentMaterialFirst ?? 0;
        ChangingModel.ContentMaterialSecond = SelectedModel?.ContentMaterialSecond ?? 0;
        ChangingModel.ContentAdditive = SelectedModel?.ContentAdditive ?? 0;
    }

    /// <summary>
    /// Проверка на существование модели
    /// </summary>
    /// <returns></returns>
    protected override async Task<bool> CheckingForExistenceAsync()
    {
        var user = await _repository.GetEntityByFilterFirstOrDefaultAsync(u =>
            u.Additive.Id == ChangingModel.Additive.Id
            && u.CompatibilityMaterial.Id == ChangingModel.CompatibilityMaterial.Id
            && Math.Abs(u.ContentMaterialFirst - ChangingModel.ContentMaterialFirst) < 0.00001
            && Math.Abs(u.ContentMaterialSecond - ChangingModel.ContentMaterialSecond) < 0.00001
            && Math.Abs(u.ContentAdditive - ChangingModel.ContentAdditive) < 0.00001);
        return user == null;
    }

    /// <summary>
    /// Проверка на корректность введенных данных
    /// </summary>
    /// <returns></returns>
    protected override bool CanAdd() => ChangingModel.Additive != null
                                        && ChangingModel.CompatibilityMaterial != null
                                        && ChangingModel != null;

    public override Task UpdateEntitiesAsync()
    {
        var materials = _materialRepository.GetEntitiesAsync().Result;
        CompatibilityMaterials = new ObservableCollection<CompatibilityMaterialModel>(_mapper.Map<ICollection<CompatibilityMaterialModel>>(materials));
        Additives = new ObservableCollection<AdditiveModel>(_mapper.Map<ICollection<AdditiveModel>>(_additiveRepository.GetEntitiesAsync().Result));

        return base.UpdateEntitiesAsync();
    }
}