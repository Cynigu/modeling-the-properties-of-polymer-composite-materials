using System;
using System.Windows.Media.Media3D;

namespace Polimer.App.Services
{
    public static class CalculatePhysicsService
    {
        #region Показатель текучести расплава

        /// <summary>
        /// Показатель текучести расплава
        /// </summary>
        /// <param name="m">средняя масса эструдируемых отрезков (?)</param>
        /// <param name="t">промежуток времени между двумя последовательными срезами отрезков (?), с</param>
        /// <returns></returns>
        public static double GetFlowMeltFluidity(double m, double t)
        {
            return 600 * m / t;
        }

        #endregion

        #region Растворимость

        /// <summary>
        /// Получить растворимость
        /// </summary>
        /// <param name="dE">необходимая энерегия для полного испарения вещества</param>
        /// <param name="v">объем вещества</param>
        /// <returns></returns>
        public static double GetSolubility(double dE, double v)
        {
            return Math.Pow((dE/v), 0.5);
        }

        #endregion

        #region Число фаз 

        /// <summary>
        /// Найти число фаз
        /// </summary>
        /// <param name="f">число степеней свободы</param>
        /// <param name="n">число компонентов в системе</param>
        /// <returns></returns>
        public static int GetNumberOfPhases(int f=2, int n=3)
        {
            return n+1-f;
        }

        #endregion


        #region Вязкость

        /// <summary>
        /// Получить вязкость 
        /// </summary>
        /// <param name="molecMassFirst">молекулярная масса эл-та</param>
        /// <param name="molecMassSecond"></param>
        /// <param name="molecMassAdditive"></param>
        /// <param name="viscosityFirst">вязкость эл-та</param>
        /// <param name="viscositySecond"></param>
        /// <param name="viscosityAdditive"></param>
        /// <returns></returns>
        public static double GetViscosity(double molecMassFirst, double molecMassSecond, double molecMassAdditive,
            double viscosityFirst, double viscositySecond, double viscosityAdditive)
        {
            var lnN = molecMassFirst * Math.Log10(viscosityFirst) + molecMassSecond * Math.Log10(viscositySecond)
                + molecMassAdditive * Math.Log10(viscosityAdditive);
            return Math.Round( lnN, 1);
        }

        #endregion

        #region Плотность/ насыпная плотность

        /// <summary>
        /// Расчет плотности материала/ насыпной плотности 
        /// </summary>
        /// <param name="totslMass">масса материала (смеси/полимера)</param>
        /// <param name="totalVolume">объем материала (смеси/полимера)</param>
        /// <returns></returns>
        public static double GetDensity(double totslMass, double totalVolume)
        {
            return Math.Round( totslMass / totalVolume, 1);
        }


        #endregion


        #region Доп методы

        /// <summary>
        /// Расчет массы материала
        /// </summary>
        /// <param name="density">плотность материала (смеси/полимера)</param>
        /// <param name="volume">объем материала (смеси/полимера)</param>
        /// <returns></returns>
        public static double GetMass(double density, double volume)
        {
            return density * volume;
        }

        /// <summary>
        /// Расчет объема материала по массе и плотности
        /// </summary>
        /// <param name="mass">масса</param>
        /// <param name="density">плотность</param>
        /// <returns></returns>
        public static double GetVolume(double mass, double density)
        {
            return mass / density;
        }

        /// <summary>
        /// Расчет объема в пространстве по длине, ширине, высоте пространства
        /// </summary>
        /// <param name="lenght">длина</param>
        /// <param name="width">ширина</param>
        /// <param name="height">высота</param>
        /// <returns></returns>
        public static double GetVolume(double lenght, double width, double height)
        {
            return lenght * width * height;
        }

        /// <summary>
        /// Расчет объема элемента от процента элемента в смеси
        /// </summary>
        /// <param name="percent">процент элемента в смеси (например: 0.5) </param>
        /// <param name="totalVolume">общий объем смеси</param>
        /// <returns></returns>
        public static double GetVolumeByPercent(double percent, double totalVolume, double totalPercent)
        {
            return percent * totalVolume / totalPercent;
        }

        #endregion


    }
}
