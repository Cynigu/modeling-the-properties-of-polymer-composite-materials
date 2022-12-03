namespace Polimer.App.ViewModel.Admin.Models;

public class PropertyUsefulProductModel : ViewModelBase, IModelAsEntity
{
    private int? _id;
    private PropertyModel _property;
    private UsefulProductModel _usefulProduct;
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

    public UsefulProductModel UsefulProduct
    {
        get => _usefulProduct;
        set => SetField(ref _usefulProduct, value);
    }

    public double Value
    {
        get => _value;
        set => SetField(ref _value, value);
    }
}