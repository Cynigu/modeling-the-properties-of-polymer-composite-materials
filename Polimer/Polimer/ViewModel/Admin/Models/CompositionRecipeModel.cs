namespace Polimer.App.ViewModel.Admin.Models;

public class CompositionRecipeModel : ViewModelBase, IModelAsEntity
{
    private int? _id;
    private MixtureModel _mixture;
    private RecipeModel _recipe;

    public int? Id
    {
        get => _id;
        set => SetField(ref _id, value);
    }

    public RecipeModel Recipe
    {
        get => _recipe;
        set => SetField(ref _recipe, value);
    }

    public MixtureModel Mixture
    {
        get => _mixture;
        set => SetField(ref _mixture, value);
    }
}