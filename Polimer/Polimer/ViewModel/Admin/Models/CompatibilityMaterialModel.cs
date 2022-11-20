namespace Polimer.App.ViewModel.Admin.Models;

public class CompatibilityMaterialModel: ViewModelBase, IModelAsEntity
{
    private int? _id;
    private MaterialModel _firstMaterial;
    private MaterialModel _secondMaterial;

    public int? Id
    {
        get => _id;
        set => SetField(ref _id, value);
    }

    public MaterialModel FirstMaterial
    {
        get => _firstMaterial;
        set => SetField(ref _firstMaterial, value);
    }

    public MaterialModel SecondMaterial
    {
        get => _secondMaterial;
        set => SetField(ref _secondMaterial, value);
    }
}