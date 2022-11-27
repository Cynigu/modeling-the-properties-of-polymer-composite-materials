using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using AutoMapper;
using Polimer.App.ViewModel.Admin.Abstract;
using Polimer.App.ViewModel.Admin.Models;
using Polimer.Data.Models;
using Polimer.Data.Repository;

namespace Polimer.App.ViewModel.Admin;

public class CompatibilityMaterialViewModel 
    : TabAdminBaseViewModel<CompatibilityMaterialEntity, CompatibilityMaterialModel>
{   
    private ObservableCollection<MaterialModel> _materialsFirst;

    private CompatibilityMaterialViewModel(CompatibilityMaterialrRepository repository,
        IMapper mapper,
        MaterialRepository materialRepository) : 
        base(repository, mapper)
    {
        NameTab = "Совместимость материалов";
        ChangingModel = new CompatibilityMaterialModel();
        var materials = materialRepository.GetEntitiesAsync().Result;
        Materials = new ObservableCollection<MaterialModel>(mapper.Map<ICollection<MaterialModel>>(materials));
    }

    public static CompatibilityMaterialViewModel CreateInstance(CompatibilityMaterialrRepository repository, IMapper mapper, MaterialRepository materialRepository)
    {
        return new CompatibilityMaterialViewModel(repository, mapper, materialRepository);
    }

    public ObservableCollection<MaterialModel> Materials
    {
        get => _materialsFirst;
        set => SetField(ref _materialsFirst, value);
    }

    protected override void CopySelectedModelToChanging()
    {
        ChangingModel.Id = 0;
        ChangingModel.FirstMaterial = (Materials
            .FirstOrDefault(m => m.Id == SelectedModel?.FirstMaterial.Id) )!;
        ChangingModel.SecondMaterial = (Materials
            .FirstOrDefault(m => m.Id == SelectedModel?.SecondMaterial.Id))!;
    }

    protected override async Task<bool> CheckingForExistenceAsync()
    {
        var user = await _repository.GetEntityByFilterFirstOrDefaultAsync(u => 
            u.FirstMaterial.Id == ChangingModel.FirstMaterial.Id 
            && u.SecondMaterial.Id == ChangingModel.SecondMaterial.Id);
        return user == null;
    }

    protected override bool CanAdd() => ChangingModel.SecondMaterial != null 
                                        && ChangingModel.FirstMaterial != null
                                        && ChangingModel != null;


}

public class PropertiesViewModel : TabAdminBaseViewModel<PropertyEntity, PropertyModel>
{
    private ObservableCollection<UnitModel> _unitModels;

    private PropertiesViewModel(PropertyRepository repository,
        IMapper mapper,
        UnitRepository materialRepository) :
        base(repository, mapper)
    {
        NameTab = "Свойства";
        ChangingModel = new PropertyModel();
        var units = materialRepository.GetEntitiesAsync().Result;
        Units = new ObservableCollection<UnitModel>(mapper.Map<ICollection<UnitModel>>(units));
    }

    public static PropertiesViewModel CreateInstance(PropertyRepository repository, IMapper mapper, UnitRepository materialRepository)
    {
        return new PropertiesViewModel(repository, mapper, materialRepository);
    }

    public ObservableCollection<UnitModel> Units
    {
        get => _unitModels;
        set => SetField(ref _unitModels, value);
    }

    protected override void CopySelectedModelToChanging()
    {
        //ChangingModel.Id = 0;
        ChangingModel.Name = SelectedModel?.Name;
        ChangingModel.Unit = (Units
            .FirstOrDefault(m => m.Id == SelectedModel?.Unit.Id))!;
    }

    protected override async Task<bool> CheckingForExistenceAsync()
    {
        var user = await _repository.GetEntityByFilterFirstOrDefaultAsync(u =>
            u.Name == ChangingModel.Name);
        return user == null;
    }

    protected override bool CanAdd() => ChangingModel.Name != null
    && ChangingModel != null && ChangingModel.Unit != null;


}