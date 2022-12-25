using System;
using System.Linq;
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
        private static double GetFlowMeltFluidity(double m, double t)
        {
            return 600 * m / t;
        }

        /// <summary>
        /// Получтить ПТР 
        /// </summary>
        /// <param name="t">время в с</param>
        /// <param name="k">колиество зон</param>
        /// <param name="totalVolume"></param>
        /// <param name="percents"></param>
        /// <param name="densities"></param>
        /// <returns></returns>
        public static double GetPtr(double t, double k, double totalVolume, double[] percents, double[] densities)
        {
            // 1. Нахоим массу элементов в кг и переводим каждый элемент в граммы
            var massElemans = GetMassElementsByTotalVolume(percents, totalVolume, densities)
                .Select(x => x * 1000).ToArray();

            // 2. Находим массу смеси и делим на количество зон
            var averageMass = massElemans.Sum() / k;

            // 3. переводим время в минуты
            t /= 60;
            
            // 4. находим ПТР 
            var ptr = GetFlowMeltFluidity(averageMass, t);

            return ptr;
        }

        #endregion

        #region Растворимость

        /// <summary>
        /// Получить растворимость 
        /// </summary>
        /// <param name="constMolElements">мольные константы</param>
        /// <param name="massMolec">мономерная молекулярная масса</param>
        /// <param name="p">плотность (смеси?)</param>
        /// <returns></returns>
        private static double GetSolubility(double[] constMolElements, double massMolec, double p)
        {
            var y = constMolElements.Sum();
            return y*p/massMolec;
        }

        public static double GetSolubility(double[] constMolElements, double[] molecMass, double p, double[] percents, double totalVolume, double[] densities )
        {
            //1. находим общую молекулярную массу ???
            var m = GetTotalMolecMass(percents, molecMass) / 1000; // переводим г/моль в кг/моль


            //2. находим плотность смеси 
            var pS = GetDensity(percents, totalVolume, densities) / Math.Pow(100, 3); // переводим из кг/(м)^3  в кг/(см)^3

            //3. находим растворисомсть
            return GetSolubility(constMolElements, m, pS);
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
        /// <param name="massFractions"> массовые доли эл-тов</param>
        /// <param name="viscosityies">вязкости  элементов</param>
        /// <returns></returns>
        private static double GetViscosity(double[] massFractions,
            double[] viscosityies)
        {
            if (massFractions.Length != viscosityies.Length)
            {
                throw new ArgumentException("Ошибка при вычислении вязкости!");
            }

            double lnN = 0;

            for (int i =0; i < massFractions.Length; i++)
            {
                lnN += massFractions[i] * Math.Log10(viscosityies[i]);
            }

            var n = Math.Pow(10, lnN);

            return n;
        }

        /// <summary>
        /// Получить вязкость по поцентному соотношению эл-ов, вязкостям эл-от, плотности эл-ов и общему объему
        /// </summary>
        /// <param name="percents">проценты эл-ов (например 10%)</param>
        /// <param name="viscosityies"></param>
        /// <param name="densities"></param>
        /// <param name="totalVolume"></param>
        /// <returns></returns>
        public static double GetViscosity(double[] percents, double[] viscosityies, double[] densities, double totalVolume)
        {
            // 1.-2. Получаем массы элементов по заданным процентам элементов, плотости элементов и объема смеси
            var massElements = GetMassElementsByTotalVolume(percents, totalVolume, densities);

            // 3. Получаем массовые доли
            var totalMass = massElements.Sum();
            var massFractions = massElements.Select(massElement => GetMassFractionElement(massElement, totalMass)).ToArray();

            // 4. получаем вязкость с помощю массовых долей вещесв и вязкостей
            return GetViscosity(massFractions, viscosityies);
        }

        #endregion

        #region Плотность/ насыпная плотность

        /// <summary>
        /// Расчет плотности материала/ насыпной плотности 
        /// </summary>
        /// <param name="totslMass">масса материала (смеси/полимера)</param>
        /// <param name="totalVolume">объем материала (смеси/полимера)</param>
        /// <returns></returns>
        private static double GetDensity(double totslMass, double totalVolume)
        {
            return totslMass / totalVolume;
        }

        /// <summary>
        /// Получить плотность смеси от общего объема, процетного соотношения элементов и плотости элементов
        /// </summary>
        /// <param name="percents">процентs</param>
        /// <param name="totalVolume">Оббщий объем</param>
        /// <param name="densities">плотности элементов</param>
        /// <returns></returns>
        public static double GetDensity(double[] percents,
             double totalVolume, double[] densities)
        {
            if (percents.Length != densities.Length)
            {
                throw new ArgumentException("Ошибка при вычислении плотности смеси.");
            }

            // 1.-2. Получить массы элементов по заданным процентам элементов, плотости элементов и объема смеси
            var mass = GetMassElementsByTotalVolume(percents, totalVolume, densities);

            // 3. Ссумировать все массы
            var totalMass = mass.Sum();

            // 4. Найти плонтность смеси
            return GetDensity(totalMass, totalVolume);
        }

        #endregion


        #region Доп методы

        /// <summary>
        /// Нахождение молекулярной массы
        /// </summary>
        /// <param name="percents">проценты содержания элементов</param>
        /// <param name="modelMass">молек масса элементов</param>
        /// <returns></returns>
        private static double GetTotalMolecMass(double[] percents, double[] modelMass)
        {
            double m = 0;
            for (int i=0; i < percents.Length; i++)
            {
                m += percents[i]/10 * modelMass[i];
            }
            return m;
        }

        /// <summary>
        /// Получть массовую долю элемента
        /// </summary>
        /// <param name="massElement">масса вещества</param>
        /// <param name="totalMass">общая масса</param>
        /// <returns></returns>
        private static double GetMassFractionElement(double massElement, double totalMass)
        {
            return massElement / totalMass;
        }

        /// <summary>
        /// Получить массы элементов
        /// </summary>
        /// <param name="percents">процентное соотошение элементов</param>
        /// <param name="totalVolume">общий объем</param>
        /// <param name="densities">плотности жлементов</param>
        /// <returns></returns>
        private static double[] GetMassElementsByTotalVolume(double[] percents, double totalVolume, double[] densities)
        {
            var totalPercent = percents.Sum();

            // 1. Рассчитать объем для кажого элемента из значений рецептуры и объема полезного изделия 
            var volumes = percents.Select(p => GetVolumeByPercent(p, totalVolume, totalPercent)).ToArray();

            // 2. Рассчитать массу для каждого элемента по объему для каждого элемента и плотности каждого элемента
            double[] mass = new double[volumes.Length];

            for (int i = 0; i < mass.Length; i++)
            {
                mass[i] = GetMass(densities[i], volumes[i]);
            }

            return mass;
        }


        /// <summary>
        /// Расчет массы материала
        /// </summary>
        /// <param name="density">плотность материала (смеси/полимера)</param>
        /// <param name="volume">объем материала (смеси/полимера)</param>
        /// <returns></returns>
        private static double GetMass(double density, double volume)
        {
            return density * volume;
        }


        /// <summary>
        /// Расчет объема элемента от процента элемента в смеси
        /// </summary>
        /// <param name="percent">процент элемента в смеси (например: 0.5) </param>
        /// <param name="totalVolume">общий объем смеси</param>
        /// <returns></returns>
        private static double GetVolumeByPercent(double percent, double totalVolume, double totalPercent)
        {
            return percent * totalVolume / totalPercent;
        }

        #endregion


    }
}
