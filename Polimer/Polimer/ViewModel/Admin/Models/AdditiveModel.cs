namespace Polimer.App.ViewModel.Admin.Models;

public class AdditiveModel : ViewModelBase, IModelAsEntity
{
    private int? _id;
    private string? _name;
    private string? _russianName;
    private string? _quickName;
    private string? _englishName;

    public int? Id
    {
        get => _id;
        set => SetField(ref _id, value);
    }

    public string? RussianName
    {
        get => _russianName;
        set => SetField(ref _russianName, value);
    }

    public string? QuickName
    {
        get => _quickName;
        set => SetField(ref _quickName, value);
    }

    public string? EnglishName
    {
        get => _englishName;
        set => SetField(ref _englishName, value);
    }
}