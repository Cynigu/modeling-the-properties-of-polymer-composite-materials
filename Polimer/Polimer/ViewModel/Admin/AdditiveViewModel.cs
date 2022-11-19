using System.Threading.Tasks;
using AutoMapper;
using Polimer.App.ViewModel.Admin.Abstract;
using Polimer.App.ViewModel.Admin.Models;
using Polimer.Data.Models;
using Polimer.Data.Repository;

namespace Polimer.App.ViewModel.Admin;

public sealed class AdditiveViewModel : TabAdminBaseViewModel<AdditiveEntity, AdditiveModel>
{
    private AdditiveViewModel(AdditiveRepository userRepository, IMapper mapper) : base(userRepository, mapper)
    {
        NameTab = "Добавки";
        ChangingModel = new AdditiveModel();
    }

    public static AdditiveViewModel CreateInstance(AdditiveRepository userRepository, IMapper mapper)
    {
        return new AdditiveViewModel(userRepository, mapper);
    }

    protected override async Task<bool> CheckingForExistenceAsync()
    {
        var user = await _repository.GetEntityByFilterFirstOrDefaultAsync(u => u.Name == ChangingModel.Name);
        return user == null;
    }

    protected override bool CanAdd() => !string.IsNullOrWhiteSpace(ChangingModel?.Name);


}