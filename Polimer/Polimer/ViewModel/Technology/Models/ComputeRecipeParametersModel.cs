using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polimer.App.ViewModel.Admin.Models;

namespace Polimer.App.ViewModel.Technology.Models
{
    public class ComputeRecipeParametersModel: ViewModelBase
    {
        private double _density;
        private RecipeModel _recipe;
        private double _viscosity;
        private int _numberOfPhases;
        private double _totalVolume;
        private double _solubility;
        private double _ptr;
        private double _nasDensity;
        private UsefulProductModel _usefulProduct;

        public UsefulProductModel UsefulProduct
        {
            get => _usefulProduct;
            set => SetField(ref _usefulProduct, value);
        }

        public RecipeModel Recipe
        {
            get => _recipe;
            set => SetField(ref _recipe, value);
        }

        /// <summary>
        /// Объем
        /// </summary>
        public double TotalVolume
        {
            get => _totalVolume;
            set => SetField(ref _totalVolume, value);
        }

        /// <summary>
        /// Плотность смеси
        /// </summary>
        public double Density
        {
            get => _density;
            set => SetField(ref _density, value);
        }

        /// <summary>
        /// Вязкость
        /// </summary>
        public double Viscosity
        {
            get => _viscosity;
            set => SetField(ref _viscosity, value);
        }

        /// <summary>
        /// Число фаз
        /// </summary>
        public int NumberOfPhases
        {
            get => _numberOfPhases;
            set => SetField(ref _numberOfPhases, value);
        }

        /// <summary>
        /// Растворимость
        /// </summary>
        public double Solubility
        {
            get => _solubility;
            set => SetField(ref _solubility, value);
        }

        /// <summary>
        /// Показатель тякучести расплава
        /// </summary>
        public double Ptr
        {
            get => _ptr;
            set => SetField(ref _ptr, value);
        }

        /// <summary>
        /// Насыпная плотность
        /// </summary>
        public double NasDensity
        {
            get => _nasDensity;
            set => SetField(ref _nasDensity, value);
        }
    }
}
