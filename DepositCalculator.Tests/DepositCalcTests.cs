using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DepositCalculator;

namespace DepositCalculator.Tests
{
    [TestClass]
    public class DepositCalcTests
    {

        /// <summary>
        /// TC_FUNC_1: Расчёт простых процентов с корректными данными.
        /// 100000 руб., 12% годовых, 6 месяцев.
        /// S = 100000 * (1 + 0.12 * 6/12) = 106000.
        /// </summary>
        [TestMethod]
        public void CalculateSimple_ValidInput_ReturnsCorrectResult()
        {
            var result = DepositCalc.CalculateSimple(100000, 12, 6);

            Assert.AreEqual(106000.00, result.TotalAmount, 0.01);
            Assert.AreEqual(6000.00, result.Income, 0.01);
        }

        /// <summary>
        /// TC_VAL_2: Простые проценты при ставке 0%.
        /// Доход должен быть 0, итого = начальная сумма.
        /// </summary>
        [TestMethod]
        public void CalculateSimple_ZeroRate_ReturnsOriginalAmount()
        {
            var result = DepositCalc.CalculateSimple(50000, 0, 12);

            Assert.AreEqual(50000.00, result.TotalAmount, 0.01);
            Assert.AreEqual(0.00, result.Income, 0.01);
        }

        /// <summary>
        /// Простые проценты: 1 месяц, проверка минимального срока.
        /// 200000 руб., 6%, 1 мес. => S = 200000 * (1 + 0.06/12) = 201000.
        /// </summary>
        [TestMethod]
        public void CalculateSimple_OneMonth_ReturnsCorrectResult()
        {
            var result = DepositCalc.CalculateSimple(200000, 6, 1);

            Assert.AreEqual(201000.00, result.TotalAmount, 0.01);
            Assert.AreEqual(1000.00, result.Income, 0.01);
        }

        //сложные 

        /// <summary>
        /// TC_FUNC_2: Расчёт сложных процентов с корректными данными.
        /// 100000 руб., 12% годовых, 6 месяцев.
        /// S = 100000 * (1 + 0.01)^6 = 106152.02.
        /// </summary>
        [TestMethod]
        public void CalculateCompound_ValidInput_ReturnsCorrectResult()
        {
            var result = DepositCalc.CalculateCompound(100000, 12, 6);

            Assert.AreEqual(106152.02, result.TotalAmount, 0.01);
            Assert.AreEqual(6152.02, result.Income, 0.01);
        }

        /// <summary>
        /// Сложные проценты при ставке 0%.
        /// </summary>
        [TestMethod]
        public void CalculateCompound_ZeroRate_ReturnsOriginalAmount()
        {
            var result = DepositCalc.CalculateCompound(50000, 0, 12);

            Assert.AreEqual(50000.00, result.TotalAmount, 0.01);
            Assert.AreEqual(0.00, result.Income, 0.01);
        }

        /// <summary>
        /// Сложные проценты всегда >= простых при одинаковых параметрах (срок > 1).
        /// </summary>
        [TestMethod]
        public void CompoundInterest_GreaterOrEqual_SimpleInterest()
        {
            var simple = DepositCalc.CalculateSimple(100000, 12, 6);
            var compound = DepositCalc.CalculateCompound(100000, 12, 6);

            Assert.IsTrue(compound.TotalAmount >= simple.TotalAmount,
                string.Format("Сложные ({0}) должны быть >= простых ({1})",
                    compound.TotalAmount, simple.TotalAmount));
        }

        /// <summary>
        /// Сложные проценты: 12 месяцев, годовой срок.
        /// 500000 руб., 10%, 12 мес. => S = 500000 * (1 + 0.10/12)^12 ≈ 552340.95.
        /// </summary>
        [TestMethod]
        public void CalculateCompound_12Months_ReturnsCorrectResult()
        {
            var result = DepositCalc.CalculateCompound(500000, 10, 12);

            Assert.AreEqual(552340.95, result.TotalAmount, 0.01);
            Assert.AreEqual(52340.95, result.Income, 0.01);
        }

        //ВАЛИДАЦИЯ 

        /// <summary>
        /// TC_VAL_1: Отрицательная сумма вклада — должна быть ошибка.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateSimple_NegativePrincipal_ThrowsException()
        {
            DepositCalc.CalculateSimple(-5000, 10, 12);
        }

        /// <summary>
        /// Нулевая сумма вклада — должна быть ошибка.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateSimple_ZeroPrincipal_ThrowsException()
        {
            DepositCalc.CalculateSimple(0, 10, 12);
        }

        /// <summary>
        /// Отрицательная ставка — должна быть ошибка.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateCompound_NegativeRate_ThrowsException()
        {
            DepositCalc.CalculateCompound(100000, -5, 12);
        }

        /// <summary>
        /// Нулевой срок — должна быть ошибка.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateSimple_ZeroMonths_ThrowsException()
        {
            DepositCalc.CalculateSimple(100000, 10, 0);
        }

        /// <summary>
        /// Отрицательный срок — должна быть ошибка.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateCompound_NegativeMonths_ThrowsException()
        {
            DepositCalc.CalculateCompound(100000, 10, -3);
        }
    }
}
