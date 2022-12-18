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

public class PropertyAdditiveViewModel
    : TabAdminBaseViewModel<PropertyAdditiveEntity, PropertyAdditiveModel>
{
    private readonly AdditiveRepository _additiveRepository;
    private readonly PropertyRepository _propertyRepository;
    private ObservableCollection<AdditiveModel> _additives;
    private ObservableCollection<PropertyModel> _properties;

    private PropertyAdditiveViewModel(PropertyAdditiveRepository repository,
        IMapper mapper,
        AdditiveRepository additiveRepository,
        PropertyRepository propertyRepository) :
        base(repository, mapper)
    {
        _additiveRepository = additiveRepository;
        _propertyRepository = propertyRepository;
        NameTab = "Свойства добавок";
        ChangingModel = new PropertyAdditiveModel();
        var materials = additiveRepository.GetEntitiesAsync().Result;
        Additives = new ObservableCollection<AdditiveModel>(mapper.Map<ICollection<AdditiveModel>>(materials));
        Properties = new ObservableCollection<PropertyModel>(mapper.Map<ICollection<PropertyModel>>(propertyRepository.GetEntitiesAsync().Result));
    }

    public static PropertyAdditiveViewModel CreateInstance(PropertyAdditiveRepository repository, IMapper mapper,
        AdditiveRepository materialRepository, PropertyRepository propertyRepository)
    {
        return new PropertyAdditiveViewModel(repository, mapper, materialRepository, propertyRepository);
    }

    public ObservableCollection<AdditiveModel> Additives
    {
        get => _additives;
        set => SetField(ref _additives, value);
    }

    public ObservableCollection<PropertyModel> Properties
    {
        get => _properties;
        set => SetField(ref _properties, value);
    }


    protected override void CopySelectedModelToChanging()
    {
        //ChangingModel.Id = 0;
        ChangingModel.Additive = (Additives
            .FirstOrDefault(m => m.Id == SelectedModel?.Additive.Id))!;
        ChangingModel.Property = (Properties
            .FirstOrDefault(m => m.Id == SelectedModel?.Property.Id))!;
        ChangingModel.Value = SelectedModel?.Value ?? 0;
    }

    protected override async Task<bool> CheckingForExistenceAsync()
    {
        var user = await _repository.GetEntityByFilterFirstOrDefaultAsync(u =>
            u.Property.Id == ChangingModel.Property.Id
            && u.Additive.Id == ChangingModel.Additive.Id);
        return user == null;
    }

    protected override bool CanAdd() => ChangingModel.Property != null
                                        && ChangingModel.Additive != null
                                        && ChangingModel != null;

    public override Task UpdateEntitiesAsync()
    {
        var materials = _additiveRepository.GetEntitiesAsync().Result;
        Additives = new ObservableCollection<AdditiveModel>(_mapper.Map<ICollection<AdditiveModel>>(materials));
        Properties = new ObservableCollection<PropertyModel>(_mapper.Map<ICollection<PropertyModel>>(_propertyRepository.GetEntitiesAsync().Result));
        return base.UpdateEntitiesAsync();
    }
}