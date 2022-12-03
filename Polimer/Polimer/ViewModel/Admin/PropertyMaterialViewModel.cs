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
        ChangingModel.Value = SelectedModel?.Value ?? 0;
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