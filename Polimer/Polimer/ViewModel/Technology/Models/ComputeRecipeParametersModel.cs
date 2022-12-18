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

        public RecipeModel Recipe
        {
            get => _recipe;
            set => SetField(ref _recipe, value);
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
    }
}
