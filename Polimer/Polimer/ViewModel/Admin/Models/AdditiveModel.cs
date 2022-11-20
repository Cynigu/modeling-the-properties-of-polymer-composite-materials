namespace Polimer.App.ViewModel.Admin.Models;

public class AdditiveModel : ViewModelBase, IModelAsEntity
{
    private int? _id;
    private string? _name;

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
}