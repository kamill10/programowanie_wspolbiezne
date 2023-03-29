using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Solution1;

namespace CalculatorApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Calculator calculator = new Calculator();

       

        public MainWindow()
        {
        InitializeComponent();
            
           
        }

        private void wynikClick(object sender, RoutedEventArgs e)
        {
            var value1 = int.Parse(podstawa.Text);
            var value2= int.Parse(wykladnik.Text);
            int wynik = calculator.pov(value1, value2);
            pokazWynik.Text = wynik.ToString();
            pokazWynik.FontSize = 50;
            pokazWynik.Foreground = Brushes.Red;
           
        }

        private void podstawa_TextChanged(object sender, TextChangedEventArgs e)
        {
            podstawa.HorizontalContentAlignment = HorizontalAlignment.Center;
            podstawa.VerticalContentAlignment =VerticalAlignment.Center;
        }

        private void Wykladnik_TextChanged(object sender, TextChangedEventArgs e)
        {
            wykladnik.HorizontalContentAlignment = HorizontalAlignment.Center;
            wykladnik.VerticalContentAlignment = VerticalAlignment.Center;
        }
    }
}
