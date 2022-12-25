using System.Collections.Generic;
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
    private readonly RecipeRepository _recipeRepository;
    private ObservableCollection<RecipeModel> _recipes;

    private UsefulProductViewModel(UsefulProductRepository repository,
        IMapper mapper, RecipeRepository recipeRepository) :
        base(repository, mapper)
    {
        _recipeRepository = recipeRepository;
        NameTab = "Полезная продукция";
        ChangingModel = new UsefulProductModel();
        Recipes = new ObservableCollection<RecipeModel>( _mapper.Map<ICollection<RecipeModel>>( recipeRepository.GetEntitiesAsync().Result));
    }

    public static UsefulProductViewModel CreateInstance(UsefulProductRepository repository, IMapper mapper, RecipeRepository recipeRepository)
    {
        return new UsefulProductViewModel(repository, mapper, recipeRepository);
    }

    public ObservableCollection<RecipeModel> Recipes
    {
        get => _recipes;
        set => SetField(ref _recipes, value);
    }

    protected override async Task<bool> CheckingForExistenceAsync()
    {
        var user = await _repository.GetEntityByFilterFirstOrDefaultAsync(u => u.Name == ChangingModel.Name);
        return user == null;
    }

    protected override bool CanAdd() => !string.IsNullOrWhiteSpace(ChangingModel.Name);

    public override async Task UpdateEntitiesAsync()
    {
        Recipes = new ObservableCollection<RecipeModel>(_mapper.Map<ICollection<RecipeModel>>(_recipeRepository.GetEntitiesAsync().Result));

        await base.UpdateEntitiesAsync();
    }
}