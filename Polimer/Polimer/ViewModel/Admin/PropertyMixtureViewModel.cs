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