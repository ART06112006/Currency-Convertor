using CurrencyApp.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CurrencyApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async Task<CurrencyContainer?> GetCurrencyContainerAsync()
        {
            return await Task.Run(() =>
            {
                using (var webClient = new WebClient())
                {
                    var apiResult = webClient.DownloadString("https://api.privatbank.ua/p24api/exchange_rates?json&date=01.12.2014");
                    if (apiResult != null)
                    {
                        return JsonConvert.DeserializeObject<CurrencyContainer>(apiResult);
                    }
                    return null;
                }
            });
        }

        private async void GetCurrencyButton_Click(object sender, RoutedEventArgs e)
        {
            var currencyType = (CurrencyComboBox?.SelectedItem as ComboBoxItem)?.Content?.ToString();

            if (currencyType != null)
            {
                var currencyContainer = await GetCurrencyContainerAsync();

                SourceTextBlock.Text = currencyContainer?.Bank;
                DateTextBlock.Text = currencyContainer?.Date;
                PurchaseCurrencyTextBlock.Text = currencyContainer?.ExchangeRate?.FirstOrDefault(x => x.Currency == currencyType)?.PurchaseRateNb.ToString();
                SaleCurrencyTextBlock.Text = currencyContainer?.ExchangeRate?.FirstOrDefault(x => x.Currency == currencyType)?.SaleRateNb.ToString();

                MessageBox.Show("Data is updated!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Select a currency!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}