using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polimer.App.ViewModel.Admin.Models
{
    public class MaterialModel : ViewModelBase, IModelAsEntity
    {
        private int? _id;
        private string? _englishName;
        private string? _quickName;
        private string? _russianName;

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
}
