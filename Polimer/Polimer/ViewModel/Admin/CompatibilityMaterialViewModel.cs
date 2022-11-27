using AutoMapper;
using Polimer.App.ViewModel.Admin.Abstract;
using Polimer.App.ViewModel.Admin.Models;
using Polimer.Data.Models;
using Polimer.Data.Repository;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

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