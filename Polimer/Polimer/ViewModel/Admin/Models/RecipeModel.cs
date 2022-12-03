using System.ComponentModel.DataAnnotations.Schema;

namespace Polimer.App.ViewModel.Admin.Models;

public class RecipeModel : ViewModelBase, IModelAsEntity
{
    private int? _id;
    private AdditiveModel _additive;
    private CompatibilityMaterialModel _compatibilityMaterial;
    private double _contentMaterialSecond;
    private double _contentMaterialFirst;
    private double _contentAdditive;

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

    public CompatibilityMaterialModel CompatibilityMaterial
    {
        get => _compatibilityMaterial;
        set => SetField(ref _compatibilityMaterial, value);
    }

    public double ContentMaterialFirst
    {
        get => _contentMaterialFirst;
        set => SetField(ref _contentMaterialFirst, value);
    }

    public double ContentMaterialSecond
    {
        get => _contentMaterialSecond;
        set => SetField(ref _contentMaterialSecond, value);
    }

    public double ContentAdditive
    {
        get => _contentAdditive;
        set => SetField(ref _contentAdditive, value);
    }
}