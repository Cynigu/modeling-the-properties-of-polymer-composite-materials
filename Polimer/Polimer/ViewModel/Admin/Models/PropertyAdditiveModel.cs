namespace Polimer.App.ViewModel.Admin.Models;

public class PropertyAdditiveModel : ViewModelBase, IModelAsEntity
{
    private int? _id;
    private PropertyModel _property;
    private AdditiveModel _additive;
    private double _value;

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

    public AdditiveModel Additive
    {
        get => _additive;
        set => SetField(ref _additive, value);
    }

    public double Value
    {
        get => _value;
        set => SetField(ref _value, value);
    }
}