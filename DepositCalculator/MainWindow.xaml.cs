using System;
using System.Globalization;
using System.Windows;

namespace DepositCalculator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnCalculate_Click(object sender, RoutedEventArgs e)
        {
            ClearResults();

            // Парсинг ввода
            if (!double.TryParse(TbPrincipal.Text.Replace(',', '.'),
                    NumberStyles.Any, CultureInfo.InvariantCulture, out double principal))
            {
                ShowError("Введите корректные числовые значения.");
                return;
            }

            if (!double.TryParse(TbRate.Text.Replace(',', '.'),
                    NumberStyles.Any, CultureInfo.InvariantCulture, out double rate))
            {
                ShowError("Введите корректные числовые значения.");
                return;
            }

            if (!int.TryParse(TbMonths.Text, out int months))
            {
                ShowError("Введите корректные числовые значения.");
                return;
            }

            try
            {
                DepositResult result;
                string type;

                if (RbSimple.IsChecked == true)
                {
                    result = DepositCalc.CalculateSimple(principal, rate, months);
                    type = "Простые проценты";
                }
                else
                {
                    result = DepositCalc.CalculateCompound(principal, rate, months);
                    type = "Сложные проценты";
                }

                TbResultTitle.Text = "Результат (" + type + ")";
                TbIncome.Text = string.Format("Доход: {0:N2} руб.", result.Income);
                TbTotal.Text = string.Format("Итого: {0:N2} руб.", result.TotalAmount);
            }
            catch (ArgumentException ex)
            {
                ShowError(ex.Message);
            }
        }

        private void ClearResults()
        {
            TbResultTitle.Text = "Результат";
            TbIncome.Text = "";
            TbTotal.Text = "";
            TbError.Text = "";
        }

        private void ShowError(string message)
        {
            TbError.Text = message;
            TbIncome.Text = "";
            TbTotal.Text = "";
        }
    }
}
