using System;

namespace DepositCalculator
{
    /// <summary>
    /// Результат расчёта вклада.
    /// </summary>
    public class DepositResult
    {
        /// <summary>Итоговая сумма (вклад + доход).</summary>
        public double TotalAmount { get; set; }

        /// <summary>Доход по вкладу.</summary>
        public double Income { get; set; }
    }

    /// <summary>
    /// Калькулятор доходности вклада.
    /// Поддерживает расчёт простых и сложных процентов.
    /// </summary>
    public static class DepositCalc
    {
        /// <summary>
        /// Расчёт простых процентов.
        /// Формула: S = P × (1 + r × t / 12), где
        ///   P — начальная сумма,
        ///   r — годовая ставка (в долях, напр. 0.12 для 12%),
        ///   t — срок в месяцах.
        /// Проценты начисляются на начальную сумму в конце срока.
        /// </summary>
        public static DepositResult CalculateSimple(double principal, double annualRatePercent, int months)
        {
            Validate(principal, annualRatePercent, months);

            double rate = annualRatePercent / 100.0;
            double total = principal * (1 + rate * months / 12.0);
            double income = total - principal;

            return new DepositResult
            {
                TotalAmount = Math.Round(total, 2),
                Income = Math.Round(income, 2)
            };
        }

        /// <summary>
        /// Расчёт сложных процентов (ежемесячная капитализация).
        /// Формула: S = P × (1 + r / 12) ^ t, где
        ///   P — начальная сумма,
        ///   r — годовая ставка (в долях),
        ///   t — срок в месяцах.
        /// Каждый месяц проценты начисляются на накопленную сумму.
        /// </summary>
        public static DepositResult CalculateCompound(double principal, double annualRatePercent, int months)
        {
            Validate(principal, annualRatePercent, months);

            double rate = annualRatePercent / 100.0;
            double total = principal * Math.Pow(1 + rate / 12.0, months);
            double income = total - principal;

            return new DepositResult
            {
                TotalAmount = Math.Round(total, 2),
                Income = Math.Round(income, 2)
            };
        }

        /// <summary>
        /// Валидация входных данных.
        /// </summary>
        private static void Validate(double principal, double annualRatePercent, int months)
        {
            if (principal <= 0)
                throw new ArgumentException("Сумма вклада должна быть положительным числом.");
            if (annualRatePercent < 0)
                throw new ArgumentException("Процентная ставка не может быть отрицательной.");
            if (months <= 0)
                throw new ArgumentException("Срок вклада должен быть больше нуля.");
        }
    }
}
