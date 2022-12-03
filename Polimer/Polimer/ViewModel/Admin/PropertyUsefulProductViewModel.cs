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

public class PropertyUsefulProductViewModel
    : TabAdminBaseViewModel<PropertyUsefulProductEntity, PropertyUsefulProductModel>
{
    private ObservableCollection<UsefulProductModel> _usefulProducts;
    private ObservableCollection<PropertyModel> _properties;

    private PropertyUsefulProductViewModel(PropertyUsefulProductRepository repository,
        IMapper mapper,
        UsefulProductRepository materialRepository,
        PropertyRepository propertyRepository) :
        base(repository, mapper)
    {
        NameTab = "Свойства полезной продукции";
        ChangingModel = new();
        var materials = materialRepository.GetEntitiesAsync().Result;
        UsefulProducts = new ObservableCollection<UsefulProductModel>(mapper.Map<ICollection<UsefulProductModel>>(materials));
        Properties = new ObservableCollection<PropertyModel>(mapper.Map<ICollection<PropertyModel>>(propertyRepository.GetEntitiesAsync().Result));
    }

    public static PropertyUsefulProductViewModel CreateInstance(PropertyUsefulProductRepository repository, IMapper mapper,
        UsefulProductRepository materialRepository, PropertyRepository propertyRepository)
    {
        return new PropertyUsefulProductViewModel(repository, mapper, materialRepository, propertyRepository);
    }

    public ObservableCollection<UsefulProductModel> UsefulProducts
    {
        get => _usefulProducts;
        set => SetField(ref _usefulProducts, value);
    }

    public ObservableCollection<PropertyModel> Properties
    {
        get => _properties;
        set => SetField(ref _properties, value);
    }


    protected override void CopySelectedModelToChanging()
    {
        //ChangingModel.Id = 0;
        ChangingModel.UsefulProduct = (UsefulProducts
            .FirstOrDefault(m => m.Id == SelectedModel?.UsefulProduct.Id))!;
        ChangingModel.Property = (Properties
            .FirstOrDefault(m => m.Id == SelectedModel?.Property.Id))!;
        ChangingModel.Value = SelectedModel?.Value ?? 0;
    }

    protected override async Task<bool> CheckingForExistenceAsync()
    {
        var user = await _repository.GetEntityByFilterFirstOrDefaultAsync(u =>
            u.Property.Id == ChangingModel.Property.Id
            && u.UsefulProduct.Id == ChangingModel.UsefulProduct.Id);
        return user == null;
    }

    protected override bool CanAdd() => ChangingModel.Property != null
                                        && ChangingModel.UsefulProduct != null
                                        && ChangingModel != null;


}