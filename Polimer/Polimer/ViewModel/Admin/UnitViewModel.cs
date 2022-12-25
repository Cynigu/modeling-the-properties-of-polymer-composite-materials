using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AutoMapper;
using Polimer.App.ViewModel.Admin.Abstract;
using Polimer.App.ViewModel.Admin.Models;
using Polimer.Data.Models;
using Polimer.Data.Repository;

namespace Polimer.App.ViewModel.Admin;

public class UnitViewModel : TabAdminBaseViewModel<UnitEntity, UnitModel>
{
    private ObservableCollection<UnitModel> _unitModels;

    private UnitViewModel(UnitRepository repository,
        IMapper mapper) :
        base(repository, mapper)
    {
        NameTab = "Единицы измерения";
        ChangingModel = new UnitModel();
    }

    public static UnitViewModel CreateInstance(UnitRepository repository, IMapper mapper)
    {
        return new UnitViewModel(repository, mapper);
    }

    protected override async Task<bool> CheckingForExistenceAsync()
    {
        var user = await _repository.GetEntityByFilterFirstOrDefaultAsync(u =>
            u.Name == ChangingModel.Name);
        return user == null;
    }

    protected override bool CanAdd() => ChangingModel.Name != null;


}