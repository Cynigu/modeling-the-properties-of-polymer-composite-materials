using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AutoMapper;
using Polimer.App.ViewModel.Admin.Abstract;
using Polimer.App.ViewModel.Admin.Models;
using Polimer.Data.Models;
using Polimer.Data.Repository;

namespace Polimer.App.ViewModel.Admin;

public class MixtureViewModel
    : TabAdminBaseViewModel<MixtureEntity, MixtureModel>
{
    private MixtureViewModel(MixtureRepository repository,
        IMapper mapper) :
        base(repository, mapper)
    {
        NameTab = "Смеси";
        ChangingModel = new MixtureModel();
    }

    public static MixtureViewModel CreateInstance(MixtureRepository repository, IMapper mapper)
    {
        return new MixtureViewModel(repository, mapper);
    }

    
    protected override async Task<bool> CheckingForExistenceAsync()
    {
        var user = await _repository.GetEntityByFilterFirstOrDefaultAsync(u => u.Name == ChangingModel.Name);
        return user == null;
    }

    protected override bool CanAdd() => !string.IsNullOrWhiteSpace(ChangingModel.Name);
}