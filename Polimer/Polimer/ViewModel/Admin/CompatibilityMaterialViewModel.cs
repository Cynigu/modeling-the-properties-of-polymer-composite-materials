using AutoMapper;
using Polimer.App.ViewModel.Admin.Abstract;
using Polimer.App.ViewModel.Admin.Models;
using Polimer.Data.Models;
using Polimer.Data.Repository;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Polimer.App.ViewModel.Admin;

public class CompatibilityMaterialViewModel 
    : TabAdminBaseViewModel<CompatibilityMaterialEntity, CompatibilityMaterialModel>
{   
    private ObservableCollection<MaterialModel> _materialsFirst;

    private CompatibilityMaterialViewModel(CompatibilityMaterialrRepository repository,
        IMapper mapper,
        MaterialRepository materialRepository) : 
        base(repository, mapper)
    {
        NameTab = "Совместимость материалов";
        ChangingModel = new CompatibilityMaterialModel();
        var materials = materialRepository.GetEntitiesAsync().Result;
        Materials = new ObservableCollection<MaterialModel>(mapper.Map<ICollection<MaterialModel>>(materials));
    }

    public static CompatibilityMaterialViewModel CreateInstance(CompatibilityMaterialrRepository repository, IMapper mapper, MaterialRepository materialRepository)
    {
        return new CompatibilityMaterialViewModel(repository, mapper, materialRepository);
    }

    public ObservableCollection<MaterialModel> Materials
    {
        get => _materialsFirst;
        set => SetField(ref _materialsFirst, value);
    }

    protected override void CopySelectedModelToChanging()
    {
        ChangingModel.Id = 0;
        ChangingModel.FirstMaterial = (Materials
            .FirstOrDefault(m => m.Id == SelectedModel?.FirstMaterial.Id) )!;
        ChangingModel.SecondMaterial = (Materials
            .FirstOrDefault(m => m.Id == SelectedModel?.SecondMaterial.Id))!;
    }

    protected override async Task<bool> CheckingForExistenceAsync()
    {
        var user = await _repository.GetEntityByFilterFirstOrDefaultAsync(u => 
            u.FirstMaterial.Id == ChangingModel.FirstMaterial.Id 
            && u.SecondMaterial.Id == ChangingModel.SecondMaterial.Id);
        return user == null;
    }

    protected override bool CanAdd() => ChangingModel.SecondMaterial != null 
                                        && ChangingModel.FirstMaterial != null
                                        && ChangingModel != null;


}

public class PropertyMixtureViewModel
    : TabAdminBaseViewModel<PropertyMixtureEntity, PropertyMixtureModel>
{
    private ObservableCollection<MixtureModel> _mixtures;
    private ObservableCollection<PropertyModel> _properties;

    private PropertyMixtureViewModel(PropertyMixtureRepository repository,
        IMapper mapper,
        MixtureRepository materialRepository,
        PropertyRepository propertyRepository) :
        base(repository, mapper)
    {
        NameTab = "Свойства смесей";
        ChangingModel = new PropertyMixtureModel();
        var materials = materialRepository.GetEntitiesAsync().Result;
        Mixtures = new ObservableCollection<MixtureModel>(mapper.Map<ICollection<MixtureModel>>(materials));
        Properties = new ObservableCollection<PropertyModel>(mapper.Map<ICollection<PropertyModel>>(propertyRepository.GetEntitiesAsync().Result));
    }

    public static PropertyMixtureViewModel CreateInstance(PropertyMixtureRepository repository, IMapper mapper,
        MixtureRepository materialRepository, PropertyRepository propertyRepository)
    {
        return new PropertyMixtureViewModel(repository, mapper, materialRepository, propertyRepository);
    }

    public ObservableCollection<MixtureModel> Mixtures
    {
        get => _mixtures;
        set => SetField(ref _mixtures, value);
    }

    public ObservableCollection<PropertyModel> Properties
    {
        get => _properties;
        set => SetField(ref _properties, value);
    }


    protected override void CopySelectedModelToChanging()
    {
        //ChangingModel.Id = 0;
        ChangingModel.Mixture = (Mixtures
            .FirstOrDefault(m => m.Id == SelectedModel?.Mixture.Id))!;
        ChangingModel.Property = (Properties
            .FirstOrDefault(m => m.Id == SelectedModel?.Property.Id))!;
    }

    protected override async Task<bool> CheckingForExistenceAsync()
    {
        var user = await _repository.GetEntityByFilterFirstOrDefaultAsync(u =>
            u.Property.Id == ChangingModel.Property.Id
            && u.Mixture.Id == ChangingModel.Mixture.Id);
        return user == null;
    }

    protected override bool CanAdd() => ChangingModel.Property != null
                                        && ChangingModel.Mixture != null
                                        && ChangingModel != null;


}