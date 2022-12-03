using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AutoMapper;
using Polimer.App.ViewModel.Admin.Abstract;
using Polimer.App.ViewModel.Admin.Models;
using Polimer.Data.Models;
using Polimer.Data.Repository;

namespace Polimer.App.ViewModel.Admin;

public class UsefulProductViewModel
    : TabAdminBaseViewModel<UsefulProductEntity, UsefulProductModel>
{
    private UsefulProductViewModel(UsefulProductRepository repository,
        IMapper mapper) :
        base(repository, mapper)
    {
        NameTab = "Полезная продукция";
        ChangingModel = new UsefulProductModel();
    }

    public static UsefulProductViewModel CreateInstance(UsefulProductRepository repository, IMapper mapper)
    {
        return new UsefulProductViewModel(repository, mapper);
    }

    
    protected override async Task<bool> CheckingForExistenceAsync()
    {
        var user = await _repository.GetEntityByFilterFirstOrDefaultAsync(u => u.Name == ChangingModel.Name);
        return user == null;
    }

    protected override bool CanAdd() => !string.IsNullOrWhiteSpace(ChangingModel.Name);
}