namespace Polimer.App.ViewModel.Admin.Models;

public class UsefulProductModel : ViewModelBase, IModelAsEntity
{
    private string? _name;
    private int? _id;

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