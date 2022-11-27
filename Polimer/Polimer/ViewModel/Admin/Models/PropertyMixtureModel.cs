namespace Polimer.App.ViewModel.Admin.Models;

public class PropertyMixtureModel : ViewModelBase, IModelAsEntity
{
    private int? _id;
    private PropertyModel _property;
    private MixtureModel _mixture;

    public int? Id
    {
        get => _id;
        set => SetField(ref _id, value);
    }

    public PropertyModel Property
    {
        get => _property;
        set => SetField(ref _property, value);
    }

    public MixtureModel Mixture
    {
        get => _mixture;
        set => SetField(ref _mixture, value);
    }
}