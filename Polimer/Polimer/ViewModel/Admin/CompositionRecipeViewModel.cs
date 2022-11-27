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

public class CompositionRecipeViewModel
    : TabAdminBaseViewModel<CompositionRecipeEntity, CompositionRecipeModel>
{
    private ObservableCollection<MixtureModel> _mixtures;
    private ObservableCollection<RecipeModel> _recipes;

    private CompositionRecipeViewModel(CompositionRecipeRepository repository,
        IMapper mapper,
        MixtureRepository materialRepository,
        RecipeRepository recipeRepository) :
        base(repository, mapper)
    {
        NameTab = "Составы рецептур";
        ChangingModel = new ();
        var materials = materialRepository.GetEntitiesAsync().Result;
        Mixtures = new ObservableCollection<MixtureModel>(mapper.Map<ICollection<MixtureModel>>(materials));
        Recipes = new ObservableCollection<RecipeModel>(mapper.Map<ICollection<RecipeModel>>(recipeRepository.GetEntitiesAsync().Result));
    }

    public static CompositionRecipeViewModel CreateInstance(CompositionRecipeRepository repository, IMapper mapper,
        MixtureRepository materialRepository, RecipeRepository propertyRepository)
    {
        return new CompositionRecipeViewModel(repository, mapper, materialRepository, propertyRepository);
    }

    public ObservableCollection<MixtureModel> Mixtures
    {
        get => _mixtures;
        set => SetField(ref _mixtures, value);
    }

    public ObservableCollection<RecipeModel> Recipes
    {
        get => _recipes;
        set => SetField(ref _recipes, value);
    }


    protected override void CopySelectedModelToChanging()
    {
        //ChangingModel.Id = 0;
        ChangingModel.Mixture = (Mixtures
            .FirstOrDefault(m => m.Id == SelectedModel?.Mixture.Id))!;
        ChangingModel.Recipe = (Recipes
            .FirstOrDefault(m => m.Id == SelectedModel?.Recipe.Id))!;
    }

    protected override async Task<bool> CheckingForExistenceAsync()
    {
        var user = await _repository.GetEntityByFilterFirstOrDefaultAsync(u =>
            u.Recipe.Id == ChangingModel.Recipe.Id
            && u.Mixture.Id == ChangingModel.Mixture.Id);
        return user == null;
    }

    protected override bool CanAdd() => ChangingModel.Recipe != null
                                        && ChangingModel.Mixture != null
                                        && ChangingModel != null;


}