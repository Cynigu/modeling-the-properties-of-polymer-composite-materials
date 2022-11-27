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

public class PropertiesViewModel : TabAdminBaseViewModel<PropertyEntity, PropertyModel>
{
    private ObservableCollection<UnitModel> _unitModels;

    private PropertiesViewModel(PropertyRepository repository,
        IMapper mapper,
        UnitRepository materialRepository) :
        base(repository, mapper)
    {
        NameTab = "Свойства";
        ChangingModel = new PropertyModel();
        var units = materialRepository.GetEntitiesAsync().Result;
        Units = new ObservableCollection<UnitModel>(mapper.Map<ICollection<UnitModel>>(units));
    }

    public static PropertiesViewModel CreateInstance(PropertyRepository repository, IMapper mapper, UnitRepository materialRepository)
    {
        return new PropertiesViewModel(repository, mapper, materialRepository);
    }

    public ObservableCollection<UnitModel> Units
    {
        get => _unitModels;
        set => SetField(ref _unitModels, value);
    }

    protected override void CopySelectedModelToChanging()
    {
        //ChangingModel.Id = 0;
        ChangingModel.Name = SelectedModel?.Name;
        ChangingModel.Unit = (Units
            .FirstOrDefault(m => m.Id == SelectedModel?.Unit.Id))!;
    }

    protected override async Task<bool> CheckingForExistenceAsync()
    {
        var user = await _repository.GetEntityByFilterFirstOrDefaultAsync(u =>
            u.Name == ChangingModel.Name);
        return user == null;
    }

    protected override bool CanAdd() => ChangingModel.Name != null
                                        && ChangingModel != null && ChangingModel.Unit != null;


}