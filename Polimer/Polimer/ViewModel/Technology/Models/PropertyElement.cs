using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polimer.App.ViewModel.Technology.Models
{
    public class PropertyElement : ViewModelBase
    {
        private string? _russianName;
        private string? _quickName;
        private string? _englishName;
        private double _percentValue;
        private double _density;
        private double _molecMass;
        private double _viscosity;
        private double _constMol;
        private double _nMass;
        private double _nDensity;

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

        public double Density
        {
            get => _density;
            set => SetField(ref _density, value);
        }

        public double MolecMass
        {
            get => _molecMass;
            set => SetField(ref _molecMass, value);
        }

        public double Viscosity
        {
            get => _viscosity;
            set => SetField(ref _viscosity, value);
        }

        public double ConstMol
        {
            get => _constMol;
            set => SetField(ref _constMol, value);
        }

        public double KRast
        {
            get => _nDensity;
            set => SetField(ref _nDensity, value);
        }

        public double NMass
        {
            get => _nMass;
            set => SetField(ref _nMass, value);
        }
    }
}
