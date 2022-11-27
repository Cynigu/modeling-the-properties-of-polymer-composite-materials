namespace Polimer.App.ViewModel.Admin.Models;

public class RecipeModel : ViewModelBase, IModelAsEntity
{
    private int? _id;
    private AdditiveModel _additive;
    private MixtureModel _mixture;

    public int? Id
    {
        get => _id;
        set => SetField(ref _id, value);
    }

    public AdditiveModel Additive
    {
        get => _additive;
        set => SetField(ref _additive, value);
    }

    public MixtureModel Mixture
    {
        get => _mixture;
        set => SetField(ref _mixture, value);
    }
}