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


public class PropertyMaterialViewModel
    : TabAdminBaseViewModel<PropertyMaterialEntity, PropertyMaterialModel>
{
    private ObservableCollection<MaterialModel> _materials;
    private ObservableCollection<PropertyModel> _properties;

    private PropertyMaterialViewModel(PropertyMaterialRepository repository,
        IMapper mapper,
        MaterialRepository materialRepository,
        PropertyRepository propertyRepository) :
        base(repository, mapper)
    {
        NameTab = "Свойства материалов";
        ChangingModel = new PropertyMaterialModel();
        var materials = materialRepository.GetEntitiesAsync().Result;
        Materials = new ObservableCollection<MaterialModel>(mapper.Map<ICollection<MaterialModel>>(materials));
        Properties = new ObservableCollection<PropertyModel>(mapper.Map<ICollection<PropertyModel>>(propertyRepository.GetEntitiesAsync().Result));
    }

    public static PropertyMaterialViewModel CreateInstance(PropertyMaterialRepository repository, IMapper mapper,
        MaterialRepository materialRepository, PropertyRepository propertyRepository)
    {
        return new PropertyMaterialViewModel(repository, mapper, materialRepository, propertyRepository);
    }

    public ObservableCollection<MaterialModel> Materials
    {
        get => _materials;
        set => SetField(ref _materials, value);
    }

    public ObservableCollection<PropertyModel> Properties
    {
        get => _properties;
        set => SetField(ref _properties, value);
    }


    protected override void CopySelectedModelToChanging()
    {
        //ChangingModel.Id = 0;
        ChangingModel.Material = (Materials
            .FirstOrDefault(m => m.Id == SelectedModel?.Material.Id))!;
        ChangingModel.Property = (Properties
            .FirstOrDefault(m => m.Id == SelectedModel?.Property.Id))!;
    }

    protected override async Task<bool> CheckingForExistenceAsync()
    {
        var user = await _repository.GetEntityByFilterFirstOrDefaultAsync(u =>
            u.Property.Id == ChangingModel.Property.Id
            && u.Material.Id == ChangingModel.Material.Id);
        return user == null;
    }

    protected override bool CanAdd() => ChangingModel.Property != null
                                        && ChangingModel.Material != null
                                        && ChangingModel != null;


}