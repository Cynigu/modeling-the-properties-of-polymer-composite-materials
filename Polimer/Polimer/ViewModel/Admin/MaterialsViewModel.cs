using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Polimer.App.ViewModel.Admin.Abstract;
using Polimer.App.ViewModel.Admin.Models;
using Polimer.Data.Models;
using Polimer.Data.Repository;
using Polimer.Data.Repository.Abstract;

namespace Polimer.App.ViewModel.Admin
{
    public class MaterialsViewModel : TabAdminBaseViewModel<MaterialEntity, MaterialModel>
    {
        private MaterialsViewModel(MaterialRepository repository, IMapper mapper) : base(repository, mapper)
        {
            ChangingModel = new MaterialModel();
        }

        public static MaterialsViewModel CreateInstance(MaterialRepository repository, IMapper mapper)
        {
            return new MaterialsViewModel(repository, mapper);
        }

        protected override async Task<bool> CheckingForExistenceAsync()
        {
            var material = await
                _repository
                    .GetEntityByFilterFirstOrDefaultAsync(m => m.QuickName == ChangingModel.QuickName);
            return material == null;
        }

        protected override bool CanAdd()
        {
            return !string.IsNullOrWhiteSpace(ChangingModel.EnglishName)
                   && !string.IsNullOrWhiteSpace(ChangingModel.QuickName)
                   && !string.IsNullOrWhiteSpace(ChangingModel.RussianName);
        }
    }
}
