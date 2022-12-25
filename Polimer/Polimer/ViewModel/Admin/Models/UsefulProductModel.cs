namespace Polimer.App.ViewModel.Admin.Models;

public class UsefulProductModel : ViewModelBase, IModelAsEntity
{
    private string? _name;
    private int? _id;
    private RecipeModel _recipe;

    public int? Id
    {
        get => _id;
        set => SetField(ref _id, value);
    }


    public string? Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }

    public RecipeModel Recipe
    {
        get => _recipe;
        set => SetField(ref _recipe, value);
    }
}