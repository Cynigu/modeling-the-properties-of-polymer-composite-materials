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

public class RecipeViewModel
    : TabAdminBaseViewModel<RecipeEntity, RecipeModel>
{
    private ObservableCollection<MixtureModel> _mixtures;
    private ObservableCollection<AdditiveModel> _additives;

    private RecipeViewModel(RecipeRepository repository,
        IMapper mapper,
        MixtureRepository materialRepository,
        AdditiveRepository additiveRepository) :
        base(repository, mapper)
    {
        NameTab = "Рецептура";
        ChangingModel = new RecipeModel();
        var materials = materialRepository.GetEntitiesAsync().Result;
        Mixtures = new ObservableCollection<MixtureModel>(mapper.Map<ICollection<MixtureModel>>(materials));
        Additives = new ObservableCollection<AdditiveModel>(mapper.Map<ICollection<AdditiveModel>>(additiveRepository.GetEntitiesAsync().Result));
    }

    public static RecipeViewModel CreateInstance(RecipeRepository repository, IMapper mapper,
        MixtureRepository materialRepository, AdditiveRepository propertyRepository)
    {
        return new RecipeViewModel(repository, mapper, materialRepository, propertyRepository);
    }

    public ObservableCollection<MixtureModel> Mixtures
    {
        get => _mixtures;
        set => SetField(ref _mixtures, value);
    }

    public ObservableCollection<AdditiveModel> Additives
    {
        get => _additives;
        set => SetField(ref _additives, value);
    }


    protected override void CopySelectedModelToChanging()
    {
        //ChangingModel.Id = 0;
        ChangingModel.Mixture = (Mixtures
            .FirstOrDefault(m => m.Id == SelectedModel?.Mixture.Id))!;
        ChangingModel.Additive = (Additives
            .FirstOrDefault(m => m.Id == SelectedModel?.Additive.Id))!;
    }

    protected override async Task<bool> CheckingForExistenceAsync()
    {
        var user = await _repository.GetEntityByFilterFirstOrDefaultAsync(u =>
            u.Additive.Id == ChangingModel.Additive.Id
            && u.Mixture.Id == ChangingModel.Mixture.Id);
        return user == null;
    }

    protected override bool CanAdd() => ChangingModel.Additive != null
                                        && ChangingModel.Mixture != null
                                        && ChangingModel != null;


}