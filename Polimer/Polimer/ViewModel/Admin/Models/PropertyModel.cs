namespace Polimer.App.ViewModel.Admin.Models;

public class PropertyModel :  ViewModelBase, IModelAsEntity
{
    private int? _id;
    private string? _name;
    private UnitModel _unit;

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

    public virtual UnitModel Unit
    {
        get => _unit;
        set => SetField(ref _unit, value);
    }
}